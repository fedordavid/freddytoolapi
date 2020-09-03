using System;
using System.Threading.Tasks;
using Freddy.Application.Core.Events;
using Freddy.Application.Orders.Commands;
using Freddy.Host;
using Freddy.Persistence.Events;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Freddy.IntegrationTests
{
    public class Test : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly IServiceProvider _services;
        
        public Test(CustomWebApplicationFactory<Startup> factory)
        {
            _services = factory.Services;
        }

        // [Fact]
        public async Task TestEventStore()
        {
            var store = _services.GetRequiredService<EventStore>();

            var orderId = new Guid("7606CB37-57A1-4816-8F13-56A782C3120B");
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var events = await store.GetStream($"Orders/{orderId}");
            
            await store.AddToStream($"Orders/{orderId}", new Event[]
            {
                new OrderCreated(orderId, customerId),
                new ProductQtySet(orderId, productId, 4)
            });
        }
        
        // [Fact]
        public async Task TestEventStore2()
        {
            var store = _services.GetRequiredService<EventStore>();
            
            var orderId = new Guid("7606CB37-57A1-4816-8F13-56A782C3120B");
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var events = await store.GetStream($"Orders/{orderId}");
        }
    }
}