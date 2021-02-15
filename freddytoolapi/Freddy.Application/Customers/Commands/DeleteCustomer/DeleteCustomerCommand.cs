using System;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : Command
    {
        public Guid CustomerId { get; set; }

        public DeleteCustomerCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
