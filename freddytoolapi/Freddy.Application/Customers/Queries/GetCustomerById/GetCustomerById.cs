using System.Threading.Tasks;
using Freddy.Application.Core.Queries;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Application.Customers.Queries.GetCustomerById
{
    public class GetCustomerById : IExecuteQuery<GetCustomerByIdQuery, CustomerView>
    {
        private readonly ICustomerViews _customers;

        public GetCustomerById(ICustomerViews customers)
        {
            _customers = customers;
        }

        public async Task<CustomerView> Execute(GetCustomerByIdQuery query)
        {
            return await _customers.Customers.FirstOrDefaultAsync(c => c.Id == query.CustomerId);
        }
    }
}
