using System.Linq;

namespace Freddy.Application.Customers.Queries
{
    public interface ICustomerViews
    {
        public IQueryable<CustomerView> Customers { get; }
    }
}
