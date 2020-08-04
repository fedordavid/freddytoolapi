using System;
using System.Threading.Tasks;

namespace Freddy.Application.Commands.Customers
{
    public interface ICustomers
    {
        Task Add(Customer customer);
        Task Delete(Guid customerId);
    }
}
