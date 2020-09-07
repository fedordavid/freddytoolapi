using System;
using System.Threading.Tasks;
using Freddy.Application.Core.Events;
using Freddy.Application.Orders.Commands;
using Freddy.Persistence.Events;
using Freddy.Persistence.Orders;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.Persistence.Orders
{
    public class OrderCommandRepositoryTests
    {
        private readonly OrderCommandRepository _repository;
        private readonly Mock<IEventStore> _eventStore;

        public OrderCommandRepositoryTests()
        {
            _eventStore = new Mock<IEventStore>();
            _repository = new OrderCommandRepository(_eventStore.Object);
        }

        [Fact]
        public async Task ShouldReturnOrder()
        {
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var events = new Event[]
            {
                new OrderCreated(orderId, customerId),
                new ProductQtySet(orderId, productId, 42),
            };
            
            _eventStore.Setup(e => e.GetStream(It.IsAny<string>())).ReturnsAsync((events, 2));

            var order = await _repository.Get(orderId);
            
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(orderId, order.OrderId);
            Assert.Contains(productId, order.OrderItems.Keys);
        }
        
        [Fact]
        public async Task ShouldStoreOrderEvents()
        {
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var events = new OrderEvent[]
            {
                new OrderCreated(orderId, customerId),
                new ProductQtySet(orderId, productId, 42),
            };

            await _repository.Publish(events);
            
            _eventStore.Verify(e => e.AddToStream($"{nameof(Order)}/{orderId}", 1, events));
        }
    }
}