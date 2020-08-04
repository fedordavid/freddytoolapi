using Freddy.Application.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Commands.Customers.AddCustomer
{
    public class AddCustomerCommand : Command
    {
        public Guid CustomerId { get; set; }
        public CustomerInfo CustomerInfo { get; set; }

        public AddCustomerCommand(Guid customerId, CustomerInfo customerInfo)
        {
            CustomerId = customerId;
            CustomerInfo = customerInfo;
        }
    }
}
