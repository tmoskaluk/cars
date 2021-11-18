using Cars.ReadModel.Sales;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cars.IntegrationTests.Sales.ReadModel
{
    [TestFixture]
    public class OffersQueryTest : TestBase
    {
        [Test]
        public void Given_OffersQuery_When_GetOffersList_Then_Results_Is_Not_Null()
        {
            //arrange
            var offersQuery = Container.GetService<IOffersQuery>();

            //act
            var results = offersQuery.GetOffers();

            //assert
            Assert.NotNull(results);
        }
    }
}
