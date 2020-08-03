using System;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Queries.Products;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Xunit;

namespace Freddy.IntegrationTests.Controllers
{
    public static partial class ProductControllerTests
    {
        [Collection("Integration")]
        public class GetProductTests : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly HttpClient _client;
            private readonly IServiceProvider _services;

            public GetProductTests(CustomWebApplicationFactory<Startup> factory)
            {
                _client = factory.CreateClient();
                _services = factory.Services;
            }

            [Fact]
            public async Task GetAllProducts_ShouldReturnDataFromDatabase()
            {
                const string url = "api/freddy/products";

                var result = await _client.GetObjectAsync<ProductView[]>(url);

                Assert.Equal(2, result.Length);

                await using (var ctx = _services.CreateDbContext())
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
        }
    }
}
