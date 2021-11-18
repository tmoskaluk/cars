using Cars.Sales.Core.Application;
using Cars.Sales.Core.Application.DataTransferObjects;
using Cars.Sales.Core.Domain;
using Cars.Sales.Core.Domain.Entities;
using Cars.Sales.Core.Domain.Repositories;
using Cars.Sales.Core.Domain.ValueObjects;
using Cars.SharedKernel.Log;

namespace Cars.Sales.Core.Infrastructure.Services.Application
{
    public class OrdersApplicationService : IOrdersApplicationService
    {
        private readonly IAppLogger logger;
        private readonly IOrdersRepository ordersRepository;
        private readonly IOffersRepository offersRepository;
        private readonly ISalesUnitOfWork unitOfWork;

        public OrdersApplicationService(IAppLogger logger, IOrdersRepository ordersRepository, IOffersRepository offersRepository, ISalesUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.ordersRepository = ordersRepository;
            this.offersRepository = offersRepository;
            this.unitOfWork = unitOfWork;
        }

        public OrderDto PlaceOrder(OfferDto offerDto, SalesCustomerDto customerDto)
        {
            var offer = this.offersRepository.Get(offerDto.Id);
            var customer = new Customer(customerDto.CustomerId, customerDto.Name);
            var order = Order.Create(offer, customer);
            
            this.ordersRepository.Add(order);
            this.unitOfWork.Commit();

            this.logger.Info($"Order created [OrderId = {order.Id}, CustomerId = {order.Customer.CustomerId}]");
            return new OrderDto { OrderId = order.Id };
        }

        public void ApplyDiscount(int orderId, decimal discount, string comment)
        {
            var order = this.ordersRepository.Get(orderId);
            order.ApplyDiscount(discount);
            order.AddComment(comment);

            this.unitOfWork.Commit();
            this.logger.Info($"Discount applied [OrderId = {order.Id}, Discount = {discount}]");
        }
    }
}
