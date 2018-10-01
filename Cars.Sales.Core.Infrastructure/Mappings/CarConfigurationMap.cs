using Cars.Sales.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Sales.Core.Infrastructure.Mappings
{
    internal class CarConfigurationMap : IEntityTypeConfiguration<CarConfiguration>
    {
        public void Configure(EntityTypeBuilder<CarConfiguration> builder)
        {
            builder.Property(x => x.Model).HasMaxLength(CarConfiguration.MODEL_MAX_LENGTH);
            builder.Property(x => x.Color).IsRequired();
            builder.Property(x => x.Version).IsRequired();
            builder.OwnsOne(x => x.Engine);
            builder.OwnsOne(x => x.Gearbox);
        }
    }
}
