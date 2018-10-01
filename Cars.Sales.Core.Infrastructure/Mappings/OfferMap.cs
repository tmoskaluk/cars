using Cars.Sales.Core.Domain.Entities;
using Cars.Sales.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Sales.Core.Infrastructure.Mappings
{
    internal class OfferMap : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType(SalesDbContext.DB_MONEY_TYPE);
            builder.Property(x => x.ExpirationDate).IsRequired();
            builder.OwnsOne(x => x.Configuration);
        }
    }
}
