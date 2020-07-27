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
using Freddy.Persistance.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Product = Freddy.Persistance.Entities.Product;
using Freddy.Application.Queries.Customers;

namespace Freddy.IntegrationTests.Controllers
{
    [Collection("Integration")]
    public class OrderControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly IServiceProvider _services;

        public OrderControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _services = factory.Services;
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

            await using (var ctx = CreateDatabaseContext())
            {
                foreach (var product in result)
                {
                    var findProduct = await ctx.Products.FindAsync(product.Id);
                    Assert.NotNull(product);
                    Assert.Equal(product.Id, findProduct.Id);
                    Assert.Equal(product.Code, findProduct.Code);
                    Assert.Equal(product.Name, findProduct.Name);
                    Assert.Equal(product.Size, findProduct.Size);
                }
            }
        }

        [Fact]
        public async Task PostProduct_ShouldReturn201()
        {
            var url = $"api/freddy/products";
            var info = new ProductInfo("code", "name", "size");

            var response = await _client.PostObjectAsync(url, info);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
 
        [Fact]
        public async Task PostProduct_ShouldReturnLocationToGetProduct()
        {
            var url = $"api/freddy/products";
            var info = new ProductInfo("code", "name", "size");

            var response = await _client.PostObjectAsync(url, info);

            var location = response.Headers.Location;
            var returnedUrl = string.Concat(location.Segments.SkipLast(1)).Trim('/');
            var productId = location.Segments.Last();

            Assert.Equal(url, returnedUrl);
            Assert.True(Guid.TryParse(productId, out _));
        }

        [Fact]
        public async Task PostProduct_ShouldAddProductToDatabase()
        {
            var url = $"api/freddy/products";
            var info = new ProductInfo("code", "name", "size");

            var response = await _client.PostObjectAsync(url, info);

            var location = response.Headers.Location;
            var productId = new Guid(location.Segments.Last());

            await using (var ctx = CreateDatabaseContext())
            {
                var product = await ctx.Products.FindAsync(productId);
                Assert.NotNull(product);
                Assert.Equal(product.Code, info.Code);
                Assert.Equal(product.Name, info.Name);
                Assert.Equal(product.Size, info.Size);
            }
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
        public async Task DeleteProduct_ShouldDeleteProductFromDatabase()
        {
            var productId = new Guid("17088A6C-68A4-484E-901A-FA60665D99DE");
            var url = $"api/freddy/products/{productId}";

            await using (var ctx = CreateDatabaseContext())
            {
                await ctx.Products.AddAsync(new Product { Id = productId });
                await ctx.SaveChangesAsync();
            }

            await _client.DeleteAsync(url);

            await using (var ctx = CreateDatabaseContext())
            {
                var product = await ctx.Products.FindAsync(productId);
                Assert.Null(product);
            }
        }

        [Fact]
        public async Task PutProduct_ShouldReturn200()
        {
            var productId = new Guid("E8E060D6-5CFC-4009-B150-C0870CC45464");
            var updateProductUrl = $"api/freddy/products/{productId}";
            var updateProductInfo = new ProductInfo("S0WTRD3", "FREDDY training női pamut ruha- fekete", "M");
            var updateProductResponse = await _client.PutObjectAsync(updateProductUrl, updateProductInfo);

            Assert.Equal(HttpStatusCode.OK, updateProductResponse.StatusCode);
        }

        [Fact]
        public async Task PutProduct_ShouldHaveUpdatedProperties()
        {
            var productId = new Guid("E8E060D6-5CFC-4009-B150-C0870CC45464");
            var updateProductUrl = $"api/freddy/products/{productId}";
            var updateProductInfo = new ProductInfo("S0WTRD3", "FREDDY training női pamut ruha- fekete", "M");
            await _client.PutObjectAsync(updateProductUrl, updateProductInfo);

            await using (var ctx = CreateDatabaseContext())
            {
                var product = await ctx.Products.FindAsync(productId);
                Assert.Equal(updateProductInfo.Code, product.Code);
                Assert.Equal(updateProductInfo.Name, product.Name);
                Assert.Equal(updateProductInfo.Size, product.Size);
            }
        }

        private DatabaseContext CreateDatabaseContext()
            => _services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
    }
}