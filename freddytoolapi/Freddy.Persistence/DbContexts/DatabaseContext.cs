using System;
using Freddy.Persistence.Customers;
using Freddy.Persistence.Events;
using Freddy.Persistence.Products;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistence.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<EventDescriptorEntity> Events { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity()
                {
                    Id = new Guid("E8E060D6-5CFC-4009-B150-C0870CC45464"),
                    Code = "WRUP1LC001, P4",
                    Name = "Fáradt rózsaszín pamut nadrág- csípő",
                    Size = "M"
                },
                new ProductEntity()
                {
                    Id = new Guid("3B50451A-05D1-4E96-A2D7-7FF1E2CCA09F"),
                    Code = "WRUP1LC001, N",
                    Name = "Fekete pamut nadrág- csípő",
                    Size = "XL"
                });

            modelBuilder.Entity<CustomerEntity>().HasData(
                new CustomerEntity()
                {
                    Id = new Guid("8E704345-26BC-4091-A9CC-0CA052C03556"),
                    Name = "Ilosfai-Pataki Júlia",
                    Email = "ipj@humail.hu",
                    Phone = "+36 900 800 4445"
                },
                new CustomerEntity()
                {
                    Id = new Guid("2F4172F9-8537-4059-8567-31D5E36029A9"),
                    Name = "Bujdosó Réka",
                    Email = "bujdosoreka@humail.hu",
                    Phone = "+36 123 90 8999"
                });
        }
    }
}
