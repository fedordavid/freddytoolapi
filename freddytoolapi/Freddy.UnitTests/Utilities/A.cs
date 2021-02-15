using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Freddy.Application.Core.Events;
using Freddy.Application.Customers.Commands;
using Freddy.Application.Orders.Commands;
using Freddy.Application.Products.Commands;

namespace Freddy.Application.UnitTests.Utilities
{
    public static class A
    {
        public static class Product
        {
            public static Expression<Func<Products.Commands.Product, bool>> With(Guid id, ProductInfo info)
            {
                return p => p.Id == id
                            && p.Info.Code == info.Code
                            && p.Info.Name == info.Name
                            && p.Info.Size == info.Size;
            }
        }

        public static class Customer
        {
            public static Expression<Func<Customers.Commands.Customer, bool>> With(Guid id, CustomerInfo info)
            {
                return p => p.Id == id
                            && p.Info.Email == info.Email
                            && p.Info.Name == info.Name
                            && p.Info.Phone == info.Phone;
            }
        }
    }

    public static class An
    {
        public static class OrderCreated
        {
            public static Expression<Func<IEnumerable<OrderEvent>, bool>> With(Guid orderId, Guid customerId)
            {
                var expected = new[] { new Orders.Commands.OrderCreated(orderId, customerId) };
                return e => expected.SequenceEqual(e);
            }
        }
    }
}
