using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Customers.Queries;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Xunit;

namespace Freddy.IntegrationTests.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        [Collection("Integration")]
        public class GetCustomersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public GetCustomersTests(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task GetAllCustomers_ShouldReturn200()
            {
                const string url = "api/freddy/customers";

                var response = await _client.GetAsync(url);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task GetAllCustomers_ShouldReturnDataFromDatabase()
            {
                const string url = "api/freddy/customers";

                var result = await _client.GetObjectAsync<CustomerView[]>(url);

                Assert.Equal(2, result.Length);

                await using (var ctx = _services.CreateDbContext())
                {
                    foreach (var customer in result)
                    {
                        var findCustomer = await ctx.Customers.FindAsync(customer.Id);
                        Assert.NotNull(findCustomer);
                        Compare.CustomerEntityToView(findCustomer, customer);
                    }
                }
            }

            [Fact]
            public async Task GetCustomerById_ShouldReturn200()
            {
                var customerId = new Guid("8E704345-26BC-4091-A9CC-0CA052C03556");
                var url = $"api/freddy/customers/{customerId}";

                var response = await _client.GetAsync(url);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task GetCustomerBuId_ShouldReturnDataFromDatabase()
            {
                var customerId = new Guid("8E704345-26BC-4091-A9CC-0CA052C03556");
                var url = $"api/freddy/customers/{customerId}";

                var result = await _client.GetObjectAsync<CustomerView>(url);

                Assert.NotNull(result);

                await using (var ctx = _services.CreateDbContext())
                {
                    var findCustomer = await ctx.Customers.FindAsync(customerId);
                    Assert.NotNull(result);
                    Compare.CustomerEntityToView(findCustomer, result);
                }
            }

        }
    }
}