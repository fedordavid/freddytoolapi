using System;
using System.Collections.Generic;
using Freddy.Persistance.DbContexts;
using Freddy.Persistance.Entities;

namespace Freddy.IntegrationTests.TestData
{
    internal sealed class Customers
    {
        public void Initialize(DatabaseContext context)
        {
            context.Customers.RemoveRange(context.Customers);

            context.Customers.AddRange(SeedData());

            context.SaveChanges();
        }

        public IEnumerable<Customer> SeedData()
        {
            yield return new Customer()
            {
                Id = new Guid("8E704345-26BC-4091-A9CC-0CA052C03556"),
                Name = "Ilosfai-Pataki Júlia",
                Email = "ipj@humail.hu",
                Phone = "+36 900 800 4445"
            };

            yield return new Customer()
            {
                Id = new Guid("2F4172F9-8537-4059-8567-31D5E36029A9"),
                Name = "Bujdosó Réka",
                Email = "bujdosoreka@humail.hu",
                Phone = "+36 123 90 8999"
            };
        }
    }
}