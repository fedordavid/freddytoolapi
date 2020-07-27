using Freddy.Application.Core.Queries;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Application.Queries.Customers.GetAllCustomers
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
