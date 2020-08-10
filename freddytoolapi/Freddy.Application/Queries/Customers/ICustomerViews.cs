using Freddy.Application.Queries.Customers;
using System.Linq;

namespace Freddy.Application.Queries
{
    public interface ICustomerViews
    {
        public IQueryable<CustomerView> Customers { get; }
    }
}
