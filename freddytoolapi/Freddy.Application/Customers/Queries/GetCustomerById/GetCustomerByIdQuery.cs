using System;
using Freddy.Application.Core.Queries;

namespace Freddy.Application.Customers.Queries.GetCustomerById
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
