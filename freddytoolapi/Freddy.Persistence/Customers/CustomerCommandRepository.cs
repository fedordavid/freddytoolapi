using System;
using System.Threading.Tasks;
using Freddy.Application.Customers.Commands;
using Freddy.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistence.Customers
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

        public async Task<Customer> Get(Guid customerId)
        {
            var customerEntity = await _context.Customers.FindAsync(customerId);

            if (customerEntity is null)
                return null;

            var customerInfo = new CustomerInfo(customerEntity.Name, customerEntity.Email, customerEntity.Phone);
            return new Customer(customerEntity.Id, customerInfo);
        }

        public async Task Update(Customer customer)
        {
            var info = customer.Info;
            var customerEntity = await _context.Customers.FindAsync(customer.Id);

            customerEntity.Name = info.Name;
            customerEntity.Phone = info.Phone;
            customerEntity.Email = info.Email;

            await _context.SaveChangesAsync();
        }
    }
}
