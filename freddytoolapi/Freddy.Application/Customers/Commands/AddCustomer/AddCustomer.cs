using System.Threading.Tasks;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Customers.Commands.AddCustomer
{
    public class AddCustomer : IHandleCommands<AddCustomerCommand>
    {
        private readonly ICustomers _customers;

        public AddCustomer(ICustomers customers)
        {
            _customers = customers;
        }

        public Task Handle(AddCustomerCommand command)
        {
            return _customers.Add(new Customer(command.CustomerId, command.CustomerInfo));
        }
    }
}
