using Cars.ReadModel.Sales;
using Cars.Sales.Core.Application;
using Cars.Sales.Core.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace Cars.IntegrationTests.Sales.Application.Services
{
    [TestFixture]
    public class OrdersApplicationServiceTest : TestBase
    {
        [Test]
        public void Given_OrdersQuery_When_GetOrders_Then_Return_Orders_List()
        {
            //arrange
            var ordersQuery = Container.GetRequiredService<IOrdersQuery>();

            //act
            var orders = ordersQuery.GetOrderList();

            //assert
            Assert.NotNull(orders);
        }

        [Test]
        public void Given_Existing_Order_When_ApplyDiscount_Then_Discount_Should_Be_Persisted_In_Database()
        {
            //arrange
            var ordersService = Container.GetRequiredService<IOrdersApplicationService>();
            var ordersQuery = Container.GetRequiredService<IOrdersQuery>();
            var ordersRepository = Container.GetRequiredService<IOrdersRepository>();
            var orderViewModel = ordersQuery.GetOrderList().LastOrDefault();

            if (orderViewModel == null) throw new Exception("Can't find any order. Test cannot be performed!");
            var discount = orderViewModel.Price * 0.1M;

            //act
            ordersService.ApplyDiscount(orderViewModel.Id, discount, "Test discount has been applied");

            //assert
            var order = ordersRepository.Get(orderViewModel.Id);
            Assert.AreEqual(discount, order.Discount);
            Assert.AreEqual(orderViewModel.Comments + 1, order.Comments.Count());
        }
    }
}
