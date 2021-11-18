using Cars.Sales.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Sales.Core.Infrastructure.Mappings
{
    internal class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.CreationDate).IsRequired();
            builder.Property(o => o.OriginalPrice).IsRequired().HasColumnType(SalesDbContext.DB_MONEY_TYPE);
            builder.Property(o => o.Price).IsRequired().HasColumnType(SalesDbContext.DB_MONEY_TYPE);
            builder.Ignore(o => o.Discount);
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.PlannedDeliveryDate);
            builder.Property(o => o.ReceivedDate);
            builder.OwnsOne(x => x.Customer);
            builder.OwnsOne(x => x.Configuration, x =>
            {
                x.OwnsOne(c => c.Engine, c =>
                {
                    c.Property(e => e.Type).IsRequired();
                    c.Property(e => e.Capacity).IsRequired();
                    c.Property(e => e.Code).IsRequired();
                });
                x.OwnsOne(c => c.Gearbox, c =>
                {
                    c.Property(g => g.Type).IsRequired();
                    c.Property(g => g.Gears).IsRequired();
                });
            });
            builder.HasMany(x => x.Comments).WithOne(x => x.Order).OnDelete(DeleteBehavior.Cascade);
            builder.Metadata.FindNavigation(nameof(Order.Comments)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
