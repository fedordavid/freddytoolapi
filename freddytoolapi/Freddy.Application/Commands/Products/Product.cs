using System;

namespace Freddy.Application.Commands.Products
{
    public class Product
    {
        public Guid Id { get; }

        public ProductInfo Info { get; }

        public Product(Guid id, ProductInfo info)
        {
            Id = id;
            Info = info;
        }

        public Product With(ProductInfo info)
        {
            return new Product(Id, info);
        }
    }
}