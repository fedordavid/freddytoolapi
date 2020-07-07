using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Commands.Products;
using Freddy.Application.Queries.Products;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Xunit;
using Xunit.Abstractions;

namespace Freddy.IntegrationTests.Controllers
{
    public class OrderControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;

        public OrderControllerTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task GetAllProducts_ShouldReturn200()
        {
            const string url = "api/freddy/products";
            
            var response = await _client.GetAsync(url);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task GetAllProducts_ShouldReturnDataFromDatabase()
        {
            const string url = "api/freddy/products";
            
            var result = await _client.GetObjectAsync<ProductView[]>(url);
            
            Assert.Equal(2, result.Length);
        }
        
        [Fact]
        public async Task PostProduct_ShouldBeAccessibleFromReturnedLocation()
        {
            var url = $"api/freddy/products";
            var info = new ProductInfo("code", "name", "size");
            
            var response = await _client.PostObjectAsync(url, info);
            var result = await _client.GetObjectAsync<ProductView>(response.Headers.Location.LocalPath);
            
            Assert.Equal(info.Code, result.Code);
            Assert.Equal(info.Name, result.Name);
            Assert.Equal(info.Size, result.Size);
        }
    }
}