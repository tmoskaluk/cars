using Cars.Core.Base.ValueObjects;
using Cars.SharedKernel.Enums;
using System;

namespace Cars.Sales.Core.Domain.ValueObjects
{
    public class Gearbox : ValueObject<Gearbox>
    {
        private Gearbox()
        {
        }

        public Gearbox(int gears, GearboxType type)
        {
            if (gears < 5 || gears > 7) throw new ArgumentException("Wrong number of gears", nameof(gears));

            Gears = gears;
            Type = type;
        }

        public int Gears { get; private set; }

        public GearboxType Type { get; private set; }
    }
}
