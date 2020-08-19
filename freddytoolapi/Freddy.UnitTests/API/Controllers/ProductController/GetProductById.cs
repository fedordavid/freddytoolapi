using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Core.Queries;
using Freddy.Application.Products.Queries;
using Freddy.Application.Products.Queries.GetProductById;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.ProductController
{
    public static partial class ProductControllerTests
    {
        public class GetProductById : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<IQueryBus> _queryBusMock;

            public GetProductById(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _queryBusMock = new Mock<IQueryBus>();
                
                _client = builder
                    .With(_queryBusMock)
                    .CreateClient();
            }

            [Fact]
            public async Task ShouldReturn200()
            {
                var productId = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E");
                var url = $"api/freddy/products/{productId}";

                _queryBusMock.Setup(m => m.Execute(It.IsAny<Query<ProductView>>())).ReturnsAsync(new ProductView());

                var result = await _client.GetAsync(url);
            
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }

            [Fact]
            public async Task ShouldExecuteGetProductByIdQuery()
            {
                var productId = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E");
                var url = $"api/freddy/products/{productId}";

                await _client.GetAsync(url);
            
                _queryBusMock.Verify(b => b.Execute(It.Is<GetProductByIdQuery>(q => q.ProductId == productId)), Times.Once);
            }

            [Fact]
            public async Task ShouldReturnQueryResult()
            {
                var product = new ProductView {Id = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E")};
                var url = $"api/freddy/products/{product.Id}";

                _queryBusMock
                    .Setup(q => q.Execute(It.IsAny<Query<ProductView>>()))
                    .ReturnsAsync(product);
            
                var result = await _client.GetObjectAsync<ProductView>(url);
            
                Assert.NotNull(result);
                Assert.Equal(product.Id, result.Id);
            }
        }
    }
}