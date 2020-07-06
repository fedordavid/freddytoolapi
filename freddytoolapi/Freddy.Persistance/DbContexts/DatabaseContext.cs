using Freddy.Application;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistance.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Code = "WRUP1LC001, P4",
                    Name = "Fáradt rózsaszín pamut nadrág- csípő",
                    Size = "M"
                },
                new Product()
                {
                    Id = 2,
                    Code = "WRUP1LC001, N",
                    Name = "Fekete pamut nadrág- csípő",
                    Size = "XL"
                }) ;
         }
    }
}
