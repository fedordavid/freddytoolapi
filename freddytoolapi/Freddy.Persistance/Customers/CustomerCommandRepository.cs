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

        public Task Add(Customer customer)
        {
            var info = customer.Info;

            _context.Customers.Add(new CustomerEntity
            {
                Email = info.Email,
                Id = customer.Id,
                Name = info.Name,
                Phone = info.Phone
            });

            return _context.SaveChangesAsync();
        }

        public async Task Delete(Guid customerId)
        {
            var customer = await _context.Customers.FirstAsync(p => p.Id == customerId);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
