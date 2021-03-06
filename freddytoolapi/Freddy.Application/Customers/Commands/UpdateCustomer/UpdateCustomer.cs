﻿using System.Threading.Tasks;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomer : IHandleCommands<UpdateCustomerCommand>
    {
        private readonly ICustomers _customers;

        public UpdateCustomer(ICustomers customers)
        {
            _customers = customers;
        }

        public async Task Handle(UpdateCustomerCommand command)
        {
            var customer = await _customers.Get(command.CustomerId);

            if (customer is null)
                throw new NotFoundException();

            var updatedCustomer = customer.With(command.CustomerInfo);
            await _customers.Update(updatedCustomer);
        }
    }
}
