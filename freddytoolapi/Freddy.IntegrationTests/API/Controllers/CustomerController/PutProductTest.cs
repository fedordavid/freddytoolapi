using System;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Customers.Commands;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Xunit;

namespace Freddy.IntegrationTests.API.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        [Collection("Integration")]
        public class PutProductTest : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public PutProductTest(CustomWebApplicationFactory<Startup> factory)
            {

                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task ShouldUpdateCustomerInDatabase()
            {
                var customerId = new Guid("8E704345-26BC-4091-A9CC-0CA052C03556");
                var updateCustomertUrl = $"api/freddy/customers/{customerId}";
                var updateCustomerInfo = new CustomerInfo("David", "m@m.com", "+4256666666");
                await _client.PutObjectAsync(updateCustomertUrl, updateCustomerInfo);

                await using (var ctx = _services.CreateDbContext())
                {
                    var product = await ctx.Customers.FindAsync(customerId);
                    Compare.CustomerEntityToInfo(product, updateCustomerInfo);
                }

            }
        }

    }
}
