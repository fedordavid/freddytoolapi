using System;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Commands.Products;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Xunit;

namespace Freddy.IntegrationTests.Controllers
{
    public static partial class ProductControllerTests
    {
        [Collection("Integration")]
        public class PutProductTests : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public PutProductTests(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task PutProduct_ShouldHaveUpdatedProperties()
            {
                var productId = new Guid("E8E060D6-5CFC-4009-B150-C0870CC45464");
                var updateProductUrl = $"api/freddy/products/{productId}";
                var updateProductInfo = new ProductInfo("S0WTRD3", "FREDDY training női pamut ruha- fekete", "M");
                await _client.PutObjectAsync(updateProductUrl, updateProductInfo);

                await using (var ctx = _services.CreateDbContext())
                {
                    var product = await ctx.Products.FindAsync(productId);
                    Compare.ProductEntityToInfo(product, updateProductInfo);
                }
            }
        }
    }
}
