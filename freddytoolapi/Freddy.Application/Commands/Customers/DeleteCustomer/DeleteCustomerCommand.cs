using Freddy.Application.Core.Commands;
using System;

namespace Freddy.Application.Commands.Customers
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
