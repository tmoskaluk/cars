using System;
using Cars.Sales.Core.Domain.Entities;
using Cars.Sales.Core.Domain.ValueObjects;
using Cars.SharedKernel.Enums;
using NUnit.Framework;

namespace Cars.UnitTests.Sales.Domain.Entities
{
    [TestFixture]
    public class OfferTest
    {
        [Test]
        public void Given_CarConfiguration_When_CreateOffer_Then_CreatesValidObject()
        {
            //arrange
            var carConfiguration = new CarConfiguration("Octavia", new Engine("TSI_2_0_GEN3", EngineType.Petrol, 1984), new Gearbox(6, GearboxType.Automatic), 
                                                        EquipmentVersion.Limited, CarColor.RaceBlue);
            
            //act
            var offer = new Offer(carConfiguration, 150000M);

            //assert
            Assert.IsNotNull(offer);
            Assert.AreEqual(carConfiguration, offer.Configuration);
            Assert.AreEqual(150000M, offer.TotalPrice);
        }

        [Test]
        public void Given_NullCarConfiguration_When_CreateOffer_Then_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Offer(null, 100000M));
        }
    }
}
