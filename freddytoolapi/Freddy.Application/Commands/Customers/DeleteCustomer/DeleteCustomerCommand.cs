using Freddy.Application.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
