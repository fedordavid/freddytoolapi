using System.Threading.Tasks;
using Freddy.Application.Core.Queries;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomers : IExecuteQuery<GetAllCustomersQuery, CustomerView[]>
    {
        private readonly ICustomerViews _customers;

        public GetAllCustomers(ICustomerViews customers)
        {
            _customers = customers;
        }

        public Task<CustomerView[]> Execute(GetAllCustomersQuery query)
        {
            return _customers.Customers.ToArrayAsync();
        }
    }
}
