using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Commands.Products;
using Freddy.Application.Queries.Products;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
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

        [Fact]
        public async Task DeleteProduct_ShouldReturn200()
        {
            var productId = new Guid("3b50451a-05d1-4e96-a2d7-7ff1e2cca09f");
            var url = $"api/freddy/products/{productId}";

            var response = await _client.DeleteAsync(url);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_ShouldDeleteProductByProductId()
        {
            var url = $"api/freddy/products";
            var info = new ProductInfo("code", "name", "size");
            var response = await _client.PostObjectAsync(url, info);

            await _client.DeleteAsync(response.Headers.Location.LocalPath);

            var views = await _client.GetObjectAsync<ProductView[]>($"api/freddy/products");

            Assert.DoesNotContain(views, c => c.Code == info.Code);
        }

        [Fact]
        public async Task PutProduct_ShouldHaveUpdatedProperties()
        {
            var createProductUrl = $"api/freddy/products";
            var createProductInfo = new ProductInfo("S0WTRD2", "FREDDY training női pamut ruha- lila", "L");
            var createProductResponse = await _client.PostObjectAsync(createProductUrl, createProductInfo);
            var createProductResult = await _client.GetObjectAsync<ProductView>(createProductResponse.Headers.Location.LocalPath);

            var updateProductUrl = $"api/freddy/products/{createProductResult.Id}";
            var updateProductInfo = new ProductInfo("S0WTRD3", "FREDDY training női pamut ruha- fekete", "M");
            var updateProductResponse = await _client.PutObjectAsync(updateProductUrl, updateProductInfo);
            var updateProductResult = await _client.GetObjectAsync<ProductView>(updateProductResponse.Headers.Location.LocalPath);

            Assert.Equal(updateProductInfo.Code, updateProductResult.Code);
            Assert.Equal(updateProductInfo.Name, updateProductResult.Name);
            Assert.Equal(updateProductInfo.Size, updateProductResult.Size);
        }
    }
}