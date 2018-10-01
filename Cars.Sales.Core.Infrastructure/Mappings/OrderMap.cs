using Cars.Sales.Core.Domain.Entities;
using Cars.Sales.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Sales.Core.Infrastructure.Mappings
{
    internal class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.OriginalPrice).IsRequired().HasColumnType(SalesDbContext.DB_MONEY_TYPE);
            builder.Property(x => x.Price).IsRequired().HasColumnType(SalesDbContext.DB_MONEY_TYPE);
            builder.Ignore(x => x.Discount);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.PlannedDeliveryDate);
            builder.Property(x => x.ReceivedDate);
            builder.OwnsOne(x => x.Customer);
            builder.OwnsOne(x => x.Configuration, x =>
            {
                x.OwnsOne(c => c.Engine);
                x.OwnsOne(c => c.Gearbox);
            });
            builder.HasMany(x => x.Comments).WithOne(x => x.Order).OnDelete(DeleteBehavior.Cascade);
            builder.Metadata.FindNavigation(nameof(Order.Comments)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
