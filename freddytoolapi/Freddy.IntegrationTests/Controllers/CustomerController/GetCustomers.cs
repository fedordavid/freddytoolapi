using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Queries.Customers;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Xunit;

namespace Freddy.IntegrationTests.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        [Collection("Integration")]
        public class GetCustomers : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public GetCustomers(CustomWebApplicationFactory<Startup> factory)
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
                // TODO: Compare with TestData 
            }

        }
    }
}