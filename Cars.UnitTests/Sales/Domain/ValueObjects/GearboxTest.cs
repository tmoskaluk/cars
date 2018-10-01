using Cars.Sales.Core.Domain.ValueObjects;
using Cars.SharedKernel.Enums;
using NUnit.Framework;
using System;

namespace Cars.Tests.UnitTests.Sales.Domain.ValueObjects
{
    [TestFixture]
    public class GearboxTest
    {
        [Test]
        public void Given_Gearboxes_With_Same_Values_When_Comparing_Then_Are_Equal()
        {
            var gearbox1 = new Gearbox(6, GearboxType.Automatic);
            var gearbox2 = new Gearbox(6, GearboxType.Automatic);
            var gearbox3 = new Gearbox(7, GearboxType.Manual);
            var gearbox4 = new Gearbox(7, GearboxType.Manual);

            Assert.AreEqual(gearbox1, gearbox2);
            Assert.AreEqual(gearbox3, gearbox4);
            Assert.AreNotSame(gearbox1, gearbox2);
            Assert.AreNotSame(gearbox3, gearbox4);
        }

        [Test]
        public void Given_Gearboxes_With_Different_Values_When_Comparing_Then_Are_Not_Equal()
        {
            var gearbox1 = new Gearbox(6, GearboxType.Automatic);
            var gearbox2 = new Gearbox(7, GearboxType.Automatic);
            var gearbox3 = new Gearbox(6, GearboxType.Automatic);
            var gearbox4 = new Gearbox(6, GearboxType.Manual);

            Assert.AreNotEqual(gearbox1, gearbox2);
            Assert.AreNotEqual(gearbox3, gearbox4);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(4)]
        [TestCase(8)]
        public void Given_Wrong_Number_Of_Gears_When_Create_Gearbox_Then_ThrowsException(int gears)
        {
            Assert.Throws<ArgumentException>(() => new Gearbox(gears, GearboxType.Automatic));
        }
    }
}
