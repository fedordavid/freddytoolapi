using System.Threading.Tasks;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Customers.Commands.DeleteCustomer
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
