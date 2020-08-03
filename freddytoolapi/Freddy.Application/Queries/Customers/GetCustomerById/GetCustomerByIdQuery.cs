using Freddy.Application.Core.Queries;
using System;

namespace Freddy.Application.Queries.Customers.GetCustomerById
{
    public class GetCustomerByIdQuery : Query<CustomerView>
    {
        public Guid CustomerId { get; }

        public GetCustomerByIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
