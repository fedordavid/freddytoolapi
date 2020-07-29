using System;
using System.Threading.Tasks;
using Freddy.Application.Commands.Customers;
using Freddy.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistance.Customers
{
    public class CustomerCommandRepository : ICustomers
    {
        private readonly DatabaseContext _context;

        public CustomerCommandRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid customerId)
        {
            var customer = await _context.Customers.FirstAsync(p => p.Id == customerId);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
