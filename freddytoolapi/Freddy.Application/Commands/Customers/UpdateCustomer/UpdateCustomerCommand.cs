using Freddy.Application.Core.Commands;
using System;

namespace Freddy.Application.Commands.Customers.UpdateCustomer
{
    public class UpdateCustomerCommand : Command
    {
        public Guid CustomerId { get; internal set; }
        public CustomerInfo CustomerInfo { get; set; }

        public UpdateCustomerCommand(Guid customerId, CustomerInfo customerInfo)
        {
            CustomerId = customerId;
            CustomerInfo = customerInfo;
        }

    }
}
