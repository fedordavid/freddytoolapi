using System;
using System.Collections.Generic;
using Freddy.Persistence.DbContexts;
using Freddy.Persistence.Products;

namespace Freddy.IntegrationTests.TestData
{
    internal sealed class Products
    {
        public void Initialize(DatabaseContext context)
        {
            context.Products.RemoveRange(context.Products);

            context.Products.AddRange(SeedData());

            context.SaveChanges();
        }

        public IEnumerable<ProductEntity> SeedData()
        {
            yield return new ProductEntity()
            {
                Id = new Guid("E8E060D6-5CFC-4009-B150-C0870CC45464"),
                Code = "WRUP1LC001, P4",
                Name = "Fáradt rózsaszín pamut nadrág- csípő",
                Size = "M"
            };

            yield return new ProductEntity()
            {
                Id = new Guid("3B50451A-05D1-4E96-A2D7-7FF1E2CCA09F"),
                Code = "WRUP1LC001, N",
                Name = "Fekete pamut nadrág- csípő",
                Size = "XL"
            };
        }
    }
}