using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Customers.Commands;
using Xunit;

namespace Freddy.IntegrationTests.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        [Collection("Integration")]
        public class PostCustomerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public PostCustomerTests(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task PostProduct_ShouldAddCustomerToDatabase()
            {
                var url = $"api/freddy/customers";
                var info = new CustomerInfo("Firstname Lastname", "firstlast@name.com", "+9034666666");

                var response = await _client.PostObjectAsync(url, info);

                var location = response.Headers.Location;
                var customerId = new Guid(location.Segments.Last());

                await using var ctx = _services.CreateDbContext();

                var customer = await ctx.Customers.FindAsync(customerId);
                Assert.NotNull(customer);
                Compare.CustomerEntityToInfo(customer, info);
            }
        }

    }
}
