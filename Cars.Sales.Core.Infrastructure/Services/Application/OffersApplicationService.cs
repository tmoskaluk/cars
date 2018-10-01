using Cars.Sales.Core.Application;
using Cars.Sales.Core.Application.DataTransferObjects;
using Cars.Sales.Core.Domain;
using Cars.Sales.Core.Domain.Entities;
using Cars.Sales.Core.Domain.Repositories;
using Cars.Sales.Core.Domain.Services;
using Cars.Sales.Core.Domain.ValueObjects;
using Cars.SharedKernel.Log;

namespace Cars.Sales.Core.Infrastructure.Services.Application
{
    public class OffersApplicationService : IOffersApplicationService
    {
        private readonly IAppLogger logger;
        private readonly ISalesUnitOfWork unitOfWork;
        private readonly IOffersRepository offersRepository;
        private readonly IPriceCalculationService priceCalculationService;

        public OffersApplicationService(IAppLogger logger, ISalesUnitOfWork unitOfWork, IOffersRepository offersRepository, IPriceCalculationService priceCalculationService)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.offersRepository = offersRepository;
            this.priceCalculationService = priceCalculationService;
        }

        public OfferDto CreateOffer(CarConfigurationDto dto)
        {
            var carConfiguration = new CarConfiguration(dto.Model, new Engine(dto.EngineCode, dto.EngineType, dto.EngineCapacity), new Gearbox(dto.GearboxGears, dto.GearboxType), dto.Version, dto.Color);
            var price = this.priceCalculationService.CalculatePrice(carConfiguration);
            var offer = new Offer(carConfiguration, price);
            this.offersRepository.Add(offer);
            this.unitOfWork.Commit();

            this.logger.Info($"Offer created [Id = {offer.Id}]");
            return new OfferDto(offer);
        }

        public void DeleteExpiredOffers()
        {
            this.offersRepository.RemoveExpiredOffers();
            this.unitOfWork.Commit();
            this.logger.Info($"Expired offers deleted");
        }
    }
}
