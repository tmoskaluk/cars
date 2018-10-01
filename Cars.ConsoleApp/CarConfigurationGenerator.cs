using Cars.Sales.Core.Application.DataTransferObjects;
using Cars.SharedKernel.Enums;
using System;
using System.Collections.Generic;

namespace Cars.ConsoleApp
{
    public class CarConfigurationGenerator
    {
        public static CarConfigurationDto GenerateConfiguration()
        {
            var r = new Random();

            var engine = GetRandomElement(r, new[]
            {
                new Engine("TSI_1_4_GEN2", EngineType.Petrol, 1390),
                new Engine("TSI_1_4_GEN3", EngineType.Petrol, 1390),
                new Engine("TSI_1_5_GEN4", EngineType.Petrol, 1498),
                new Engine("TSI_2_0_GEN2", EngineType.Petrol, 1984),
                new Engine("TSI_2_0_GEN3", EngineType.Petrol, 1984),
                new Engine("TDI_2_0_GEN2", EngineType.Diesel, 1968),
                new Engine("TDI_2_0_GEN3", EngineType.Diesel, 1968)
            });

            var gearbox = GetRandomElement(r, new[]
            {
                new Gearbox(6, GearboxType.Manual),
                new Gearbox(6, GearboxType.Automatic),
                new Gearbox(7, GearboxType.Automatic)
            });

            return new CarConfigurationDto
            {
                Model = GetRandomElement(r, new[] { "Fabia", "Rapid", "Octavia", "Superb", "Kodiak", "Karoq" }),
                EngineType = engine.Type,
                EngineCode = engine.Code,
                EngineCapacity = engine.Capacity,
                GearboxType = gearbox.Type,
                GearboxGears = gearbox.Gears,
                Version = GetRandomElement(r, typeof(EquipmentVersion).GetEnumValues() as IList<EquipmentVersion>),
                Color = GetRandomElement(r, typeof(CarColor).GetEnumValues() as IList<CarColor>)
            };
        }

        private static T GetRandomElement<T>(Random random, IList<T> list)
        {
            var index = random.Next(list.Count);
            return list[index];
        }

        private class Gearbox
        {
            public Gearbox(int gears, GearboxType type)
            {
                Gears = gears;
                Type = type;
            }

            public int Gears { get; }

            public GearboxType Type { get; }
        }

        private class Engine
        {
            public Engine(string code, EngineType type, int capacity)
            {
                Code = code;
                Type = type;
                Capacity = capacity;
            }

            public string Code { get; }

            public EngineType Type { get; }

            public int Capacity { get; }
        }
    }
}
