using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Queries.Customers;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Freddy.Persistance.DbContexts;
using Freddy.Persistance.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Freddy.IntegrationTests.Controllers
{
    [Collection("Integration")]
    public class CustomerControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly IServiceProvider _services;

        public CustomerControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _services = factory.Services;
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturn200()
        {
            const string url = "api/freddy/customers";

            var response = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldDeleteDataFromDatabase()
        {
            {
                var customerId = new Guid("59D178FE-3727-4B67-ABA1-D573BC40C81A");
                var url = $"api/freddy/customers/{customerId}";

                await using (var ctx = CreateDatabaseContext())
                {
                    await ctx.Customers.AddAsync(new Customer { Id = customerId});
                    await ctx.SaveChangesAsync();
                }

                await _client.DeleteAsync(url);

                await using (var ctx = CreateDatabaseContext())
                {
                    var customer = await ctx.Customers.FindAsync(customerId);
                    Assert.Null(customer);
                }
            }
        }

        private DatabaseContext CreateDatabaseContext()
            => _services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
    }
}