using System;
using System.Linq;
using Freddy.Application.Core.Events;
using Freddy.Application.Orders.Commands;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Commands.Orders
{
    public class OrderTests
    {
        [Fact]
        public void CreateOrderShouldReturnCreatedEvent()
        {
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            var customer = new OrderCustomer(customerId);
            var events = customer.CreateOrder(orderId).ToArray();

            var @event = Assert.Single(events);
            var created = Assert.IsType<OrderCreated>(@event);
            Assert.Equal(customerId, created.CustomerId);
            Assert.Equal(orderId, created.OrderId);
        }
        
        [Fact]
        public void Test()
        {
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            var productA = Guid.NewGuid();
            var productB = Guid.NewGuid();

            var events = new Event[]
            {
                new OrderCreated(orderId, customerId),
                new ProductQtySet(orderId, productA, 1),
                new ProductQtySet(orderId, productB, 4),
                new ProductQtySet(orderId, productA, 2),
                new ProductRemoved(orderId, productB),
            };

            var order = Order.Initialize(events);

            Assert.Throws<Exception>(() => order.RemoveProduct(productB).ToArray());
        }
    }
}