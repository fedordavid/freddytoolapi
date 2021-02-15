using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Products.Commands;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Xunit;

namespace Freddy.IntegrationTests.API.Controllers.ProductController
{
    public static partial class ProductControllerTests
    {
        [Collection("Integration")]
        public class PostProductTests : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public PostProductTests(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task PostProduct_ShouldAddProductToDatabase()
            {
                var url = $"api/freddy/products";
                var info = new ProductInfo("code", "name", "size");

                var response = await _client.PostObjectAsync(url, info);

                var location = response.Headers.Location;
                var productId = new Guid(location.Segments.Last());

                await using var ctx = _services.CreateDbContext();

                var product = await ctx.Products.FindAsync(productId);
                Assert.NotNull(product);
                Compare.ProductEntityToInfo(product, info);
            }
        }
    }
}
