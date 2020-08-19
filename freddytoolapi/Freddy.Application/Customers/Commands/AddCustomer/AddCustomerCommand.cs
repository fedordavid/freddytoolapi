using System;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Customers.Commands.AddCustomer
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
