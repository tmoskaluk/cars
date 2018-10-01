using Cars.Sales.Core.Infrastructure.Repositories;
using Cars.SharedKernel.Sales.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Cars.ReadModel.Sales.Implementation
{
    public class OrdersQuery : IOrdersQuery
    {
        private readonly SalesDbContext context;

        public OrdersQuery(SalesDbContext context)
        {
            this.context = context;
        }

        public IList<OrderListViewModel> GetOrderList()
        {
            return this.context.Query<OrderListViewModel>().OrderBy(x => x.CreationDate).ToList();
        }
    }
}
