using Cars.Core.Base.UnitOfWork;
using Cars.Sales.Core.Domain;

namespace Cars.Sales.Core.Infrastructure
{
    public class SalesUnitOfWork : EntityFrameworkUnitOfWork<SalesDbContext>, ISalesUnitOfWork
    {
        public SalesUnitOfWork(SalesDbContext context) : base(context)
        {
        }
    }
}
