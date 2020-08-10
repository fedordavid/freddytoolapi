﻿using Freddy.Application.Commands.Customers;
using Freddy.Application.Commands.Products;
using Freddy.Application.Queries.Customers;
using Freddy.Application.Queries.Products;
using Freddy.Persistence.Customers;
using Freddy.Persistence.Products;
using Xunit;

namespace Freddy.IntegrationTests.Utilities
{
    internal static class Compare
    {
        internal static void CustomerEntityToView(CustomerEntity entity, CustomerView view)
        {
            Assert.Equal(entity.Id, view.Id);
            Assert.Equal(entity.Email, view.Email);
            Assert.Equal(entity.Name, view.Name);
            Assert.Equal(entity.Phone, view.Phone);
        }

        internal static void ProductEntityToView(ProductEntity entity, ProductView view)
        {
            Assert.Equal(entity.Id, view.Id);
            Assert.Equal(entity.Name, view.Name);
            Assert.Equal(entity.Size, view.Size);
            Assert.Equal(entity.Code, view.Code);
        }

        internal static void ProductEntityToInfo(ProductEntity entity, ProductInfo view)
        {
            Assert.Equal(entity.Name, view.Name);
            Assert.Equal(entity.Size, view.Size);
            Assert.Equal(entity.Code, view.Code);
        }

        internal static void CustomerEntityToInfo(CustomerEntity customer, CustomerInfo info)
        {
            Assert.Equal(customer.Name, info.Name);
            Assert.Equal(customer.Email, info.Email);
            Assert.Equal(customer.Phone, info.Phone);
        }
    }
}
