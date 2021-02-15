using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Freddy.Persistence.Customers;
using Xunit;

namespace Freddy.IntegrationTests.API.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        [Collection("Integration")]

        public class DeleteCustomersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public DeleteCustomersTests(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task DeleteCustomer_ShouldReturn200()
            {
                var customerId = new Guid("59D178FE-3727-4B67-ABA1-D573BC40C81A");
                var url = $"api/freddy/customers/{customerId}";

                await using (var ctx = _services.CreateDbContext())
                {
                    await ctx.Customers.AddAsync(new CustomerEntity { Id = customerId });
                    await ctx.SaveChangesAsync();
                }

                var response = await _client.DeleteAsync(url);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task DeleteCustomer_ShouldDeleteDataFromDatabase()
            {
                var customerId = new Guid("59D178FE-3727-4B67-ABA1-D573BC40C81A");
                var url = $"api/freddy/customers/{customerId}";

                await using (var ctx = _services.CreateDbContext())
                {
                    await ctx.Customers.AddAsync(new CustomerEntity { Id = customerId });
                    await ctx.SaveChangesAsync();
                }

                await _client.DeleteAsync(url);

                await using (var ctx = _services.CreateDbContext())
                {
                    var customer = await ctx.Customers.FindAsync(customerId);
                    Assert.Null(customer);
                }
            }
        }
    }
}