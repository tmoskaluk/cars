using Cars.Core.Base.UnitOfWork;
using Cars.Sales.Core.Domain;
using Cars.Sales.Core.Infrastructure.Repositories;

namespace Cars.Sales.Core.Infrastructure
{
    public class SalesUnitOfWork : EntityFrameworkUnitOfWork<SalesDbContext>, ISalesUnitOfWork
    {
        public SalesUnitOfWork(SalesDbContext context) : base(context)
        {
        }
    }
}
