using Freddy.Application.Commands.Customers;
using Freddy.Application.Commands.Products;
using System;
using System.Linq.Expressions;

namespace Freddy.Application.UnitTests.Utilities
{
    public static class A
    {
        public static class Product
        {
            public static Expression<Func<Commands.Products.Product, bool>> With(Guid id, ProductInfo info)
            {
                return p => p.Id == id
                            && p.Info.Code == info.Code
                            && p.Info.Name == info.Name
                            && p.Info.Size == info.Size;
            }
        }

        public static class Customer
        {
            public static Expression<Func<Commands.Customers.Customer, bool>> With(Guid id, CustomerInfo info)
            {
                return p => p.Id == id
                            && p.Info.Email == info.Email
                            && p.Info.Name == info.Name
                            && p.Info.Phone == info.Phone;
            }
        }
    }
}
