using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Orders.Commands;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Freddy.IntegrationTests.API.Controllers.OrdersController
{
    public static partial class OrderControllerTests
    {
        [Collection("Integration")]
        public class PostOrder : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public PostOrder(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task PostOrder_ShouldAddOrderToDatabase()
            {
                var customerId = new Guid("0B79BCFF-B202-4FD2-8464-9626296C2A3E");
                var url = $"api/freddy/customers/{customerId}/orders";

                var response = await _client.PostObjectAsync(url, new {});

                var location = response.Headers.Location;
                var orderId = new Guid(location.Segments.Last());

                await using var ctx = _services.CreateDbContext();

                var events = await ctx.Events
                    .Where(e => e.Stream == $"{nameof(Order)}/{orderId}")
                    .ToArrayAsync();
                
                Assert.NotEmpty(events);
                
                var @event = Assert.Single(events);
                
                Assert.NotNull(@event);
                Assert.Equal(nameof(OrderCreated), @event.Type);
            }
        }

    }
}
