using Cars.ReadModel.Sales;
using Cars.Sales.Core.Application;
using Cars.SharedKernel.Sales.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Cars.Sales.Core.Application.DataTransferObjects;
using Cars.DependencyInjection;
using Cars.Customers.Crud;
using Cars.Customers.Crud.Entities;
using Cars.Sales.Core.Infrastructure;
using Cars.SharedKernel.Enums;

namespace Cars.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            try
            {
                services.AddCarsProject();

                var container = services.BuildServiceProvider();
                var scopeFactory = container.GetRequiredService<IServiceScopeFactory>();

                MigrateDatabase(scopeFactory);
                RunTest(scopeFactory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine($"{Environment.NewLine}Press <Enter> to exit");
                Console.ReadKey();
            }

            //this classes shouldn't be visible because they are in domain project which is not referenced (directly) by ConsoleApp
            //CarConfiguration c = null;
            //Offer o = new Offer(c, 10000);
        }

        private static void MigrateDatabase(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<CustomersDbContext>())
                {
                    context.Database.Migrate();
                }

                using (var context = scope.ServiceProvider.GetService<SalesDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private static void DisplayOffers(IList<OfferListViewModel> offers)
        {
            var line = string.Join(string.Empty, Enumerable.Repeat("-", Console.WindowWidth));
            Console.WriteLine($"{line}Offers: {offers.Count}");

            int i = 0;
            foreach (var o in offers)
            {
                var m = $"{++i} [Id: {o.OfferId}, Date: {o.CreationDate:HH:mm:ss.fff}, Price: {o.TotalPrice:0.00}]";
                Console.WriteLine(m);
            }

            Console.WriteLine($"{line}");
        }

        private static void DisplayOrders(IList<OrderListViewModel> orders)
        {
            var line = string.Join(string.Empty, Enumerable.Repeat("-", Console.WindowWidth));
            Console.WriteLine($"{line}Orders: {orders.Count}");

            foreach (var o in orders)
            {
                var m = $"[Id: {o.Id}, Customer: {o.CustomerId}, Date: {o.CreationDate:HH:mm:ss.fff}, Status: {o.Status}, Price: {o.Price:0.00}, " +
                        $"Discount: {o.Discount:0.00}, Comments: {o.Comments}]";
                Console.WriteLine(m);
            }

            Console.WriteLine($"{line}");
        }

        private static T GetRandomElement<T>(Random random, IList<T> list)
        {
            var index = random.Next(list.Count);
            return list[index];
        }

        private static void RunTest(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var offersQuery = scope.ServiceProvider.GetService<IOffersQuery>();
                Console.WriteLine($"Offers count: {offersQuery.GetOffers().Count}{Environment.NewLine}");
            }

            OfferDto offer;
            Customer customer;
            OrderDto order;

            #region Generating offer

            using (var scope = scopeFactory.CreateScope())
            {
                var offersService = scope.ServiceProvider.GetRequiredService<IOffersApplicationService>();

                var carConfiguration = CarConfigurationGenerator.GenerateConfiguration();
                offer = offersService.CreateOffer(carConfiguration);
            }

            #endregion

            using (var scope = scopeFactory.CreateScope())
            {
                var offersQuery = scope.ServiceProvider.GetService<IOffersQuery>();
                DisplayOffers(offersQuery.GetOffers());
            }

            #region Choosing customer

            using (var scope = scopeFactory.CreateScope())
            {
                var customersContext = scope.ServiceProvider.GetRequiredService<CustomersDbContext>();

                var customers = customersContext.Customers.ToList();
                if (customers.Count == 0) throw new Exception("Can't find any customer");

                var random = new Random();
                customer = GetRandomElement(random, customers);
            }

            #endregion

            #region Placing order

            using (var scope = scopeFactory.CreateScope())
            {
                var customerDto = new SalesCustomerDto { CustomerId = customer.Id, Name = $"{customer.FirstName} {customer.LastName}" };
                var orderService = scope.ServiceProvider.GetRequiredService<IOrdersApplicationService>();
                order = orderService.PlaceOrder(offer, customerDto);
            }

            #endregion

            #region Applying discount

            if (customer.Type == CustomerType.Vip || customer.Type == CustomerType.Business)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var orderService = scope.ServiceProvider.GetRequiredService<IOrdersApplicationService>();

                    var discount = (customer.Type == CustomerType.Vip ? 0.2M : 0.1M) * offer.TotalPrice;
                    orderService.ApplyDiscount(order.OrderId, discount, $"Discount applied for '{customer.Type}' customer type in order {order.OrderId}");
                }
            }

            #endregion

            using (var scope = scopeFactory.CreateScope())
            {
                var ordersQuery = scope.ServiceProvider.GetRequiredService<IOrdersQuery>();
                DisplayOrders(ordersQuery.GetOrders());
            }
        }
    }
}
