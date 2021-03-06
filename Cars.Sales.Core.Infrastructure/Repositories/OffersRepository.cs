﻿using Cars.Core.Base.Repositories;
using Cars.Sales.Core.Domain.Entities;
using Cars.Sales.Core.Domain.Repositories;
using System;
using System.Linq;

namespace Cars.Sales.Core.Infrastructure.Repositories
{
    public class OffersRepository : AbstractRepository<Offer, Guid, SalesDbContext>, IOffersRepository
    {
        public OffersRepository(SalesDbContext context) : base(context)
        {
        }

        public void RemoveExpiredOffers()
        {
            var expiredOffers = this.Context.Offers.Where(o => o.ExpirationDate < DateTime.Now);
            this.Context.RemoveRange(expiredOffers);
        }
    }
}
