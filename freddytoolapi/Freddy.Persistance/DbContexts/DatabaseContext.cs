﻿using System;
using Freddy.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistance.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = new Guid("E8E060D6-5CFC-4009-B150-C0870CC45464"),
                    Code = "WRUP1LC001, P4",
                    Name = "Fáradt rózsaszín pamut nadrág- csípő",
                    Size = "M"
                },
                new Product()
                {
                    Id = new Guid("3B50451A-05D1-4E96-A2D7-7FF1E2CCA09F"),
                    Code = "WRUP1LC001, N",
                    Name = "Fekete pamut nadrág- csípő",
                    Size = "XL"
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = new Guid("8E704345-26BC-4091-A9CC-0CA052C03556"),
                    Name = "Ilosfai-Pataki Júlia",
                    Email = "ipj@humail.hu",
                    Phone = "+36 900 800 4445"
                });
        }
    }
}
