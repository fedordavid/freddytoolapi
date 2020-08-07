using Freddy.Application.Core.Commands;
using System.Threading.Tasks;

namespace Freddy.Application.Commands.Customers
{
    public class DeleteCustomer : IHandleCommands<DeleteCustomerCommand>
    {
        public ICustomers _customers { get; set; }

        public DeleteCustomer(ICustomers customers)
        {
            _customers = customers;
        }

        public Task Handle(DeleteCustomerCommand command)
        {
            return _customers.Delete(command.CustomerId);
        }
    }
}
