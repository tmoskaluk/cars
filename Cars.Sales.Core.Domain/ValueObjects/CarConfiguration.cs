using Cars.Core.Base.ValueObjects;
using Cars.SharedKernel.Enums;
using System;

namespace Cars.Sales.Core.Domain.ValueObjects
{
    public class CarConfiguration : ValueObject<CarConfiguration>
    {
        public const int MODEL_MAX_LENGTH = 50;
        
        private CarConfiguration()
        {
        }

        public CarConfiguration(string model, Engine engine, Gearbox gearbox, EquipmentVersion version, CarColor color)
        {
            if (string.IsNullOrWhiteSpace(model) || model.Length > MODEL_MAX_LENGTH) throw new ArgumentException(nameof(model));

            Model = model;
            Engine = engine;
            Gearbox = gearbox;
            Version = version;
            Color = color;
        }

        public string Model { get; private set; }
        
        public Engine Engine { get; private set; }

        public Gearbox Gearbox { get; private set; }

        public EquipmentVersion Version { get; private set; }
        
        public CarColor Color { get; private set; }
    }
}
