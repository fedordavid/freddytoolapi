using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Products;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers
{
    public static partial class ProductControllerTests
    {
        public class GetAllProducts : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<IQueryBus> _queryBusMock;

            public GetAllProducts(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _queryBusMock = new Mock<IQueryBus>();

                _client = builder
                    .With(_queryBusMock)
                    .CreateClient();
            }

            [Fact]
            public async Task ShouldExecuteGetProductByIdQuery()
            {
                var url = $"api/freddy/products";

                await _client.GetAsync(url);

                _queryBusMock.Verify(b => b.Execute(It.IsAny<GetAllProductsQuery>()), Times.Once);
            }

            [Fact]
            public async Task ShouldReturnQueryResult()
            {
                var url = $"api/freddy/products";

                var productView = new ProductViewBuilder();

                var products = new ProductView[]
                {
                    productView.WithId("5412D947-05DB-45AE-AA20-6ADA289FBA0E"),
                    productView.WithId("A017EFE7-D230-42BD-84F8-65574DD5AF6E")
                };

                _queryBusMock
                    .Setup(q => q.Execute(It.IsAny<Query<ProductView[]>>()))
                    .ReturnsAsync(products);

                var result = await _client.GetObjectAsync<ProductView[]>(url);

                Assert.NotNull(result);
                Assert.Equal(products, result);
            }
        }
    }
}