using Cars.Core.Base.Repositories;
using Cars.Sales.Core.Domain.Entities;
using Cars.Sales.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cars.Sales.Core.Infrastructure.Repositories
{
    public class OrdersRepository : AbstractRepository<Order, int, SalesDbContext>, IOrdersRepository
    {
        public OrdersRepository(SalesDbContext context) : base(context)
        {
        }

        public override Order Get(int id)
        {
            return Context.Orders.Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
        }
    }
}
