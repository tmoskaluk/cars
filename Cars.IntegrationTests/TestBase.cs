using Cars.Core.Base.Log;
using Cars.Customers.Crud;
using Cars.ReadModel.Sales;
using Cars.ReadModel.Sales.Implementation;
using Cars.Sales.Core.Application;
using Cars.Sales.Core.Domain;
using Cars.Sales.Core.Domain.Repositories;
using Cars.Sales.Core.Domain.Services;
using Cars.Sales.Core.Infrastructure;
using Cars.Sales.Core.Infrastructure.Repositories;
using Cars.Sales.Core.Infrastructure.Services.Application;
using Cars.Sales.Core.Infrastructure.Services.Domain;
using Cars.SharedKernel.Log;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cars.IntegrationTests
{
    public abstract class TestBase
    {
        protected TestBase()
        {
            Container = BuildDependencyInjection();
        }

        protected IServiceProvider Container { get; }

        private static IServiceProvider BuildDependencyInjection()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAppLogger, AppLogger>();
            services.AddTransient<IOffersApplicationService, OffersApplicationService>();
            services.AddTransient<IOrdersApplicationService, OrdersApplicationService>();
            services.AddTransient<IPriceCalculationService, PriceCalculationService>();
            services.AddTransient<IOffersRepository, OffersRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<IOffersQuery, OffersQuery>();
            services.AddTransient<IOrdersQuery, OrdersQuery>();
            services.AddTransient<ISalesUnitOfWork, SalesUnitOfWork>();
            services.AddScoped(ctx => new SalesDbContext());
            services.AddScoped(ctx => new CustomersDbContext());

            return services.BuildServiceProvider();
        }
    }
}
