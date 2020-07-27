using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Commands.Customers
{
    public interface ICustomers
    {
        Task Delete(Guid customerId);
    }
}
