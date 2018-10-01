using Cars.Sales.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Sales.Core.Infrastructure.Mappings
{
    internal class EngineMap : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Capacity).IsRequired();
            builder.Property(x => x.Type).IsRequired();
        }
    }
}
