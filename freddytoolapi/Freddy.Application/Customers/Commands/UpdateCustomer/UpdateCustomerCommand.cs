using System;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Customers.Commands.UpdateCustomer
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
