using System;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Freddy.Persistence.Products;
using Xunit;

namespace Freddy.IntegrationTests.API.Controllers.ProductController
{
    public static partial class ProductControllerTests
    {
        [Collection("Integration")]
        public class DeleteProductTests : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public DeleteProductTests(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task DeleteProduct_ShouldDeleteProductFromDatabase()
            {
                var productId = new Guid("17088A6C-68A4-484E-901A-FA60665D99DE");
                var url = $"api/freddy/products/{productId}";

                await using (var ctx = _services.CreateDbContext())
                {
                    await ctx.Products.AddAsync(new ProductEntity { Id = productId });
                    await ctx.SaveChangesAsync();
                }

                await _client.DeleteAsync(url);

                await using (var ctx = _services.CreateDbContext())
                {
                    var product = await ctx.Products.FindAsync(productId);
                    Assert.Null(product);
                }
            }
        }
    }
}
