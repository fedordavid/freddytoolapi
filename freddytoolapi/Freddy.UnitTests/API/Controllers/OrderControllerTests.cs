using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.API.Core;
using Freddy.Application.Commands.Products;
using Freddy.Application.Commands.Products.UpdateProduct;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Products;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers
{
    public static class OrderControllerTests
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

                var products = new[]
                {
                    new ProductView {Id = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E")},
                    new ProductView {Id = new Guid("A017EFE7-D230-42BD-84F8-65574DD5AF6E")}
                };

                _queryBusMock
                    .Setup(q => q.Execute(It.IsAny<Query<ProductView[]>>()))
                    .ReturnsAsync(products);

                var result = await _client.GetObjectAsync<ProductView[]>(url);

                Assert.NotNull(result);
                Assert.Equal(products, result);
            }
        }

        public class PostProduct : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<ICommandBus> _commandBusMock;
            private readonly Mock<IGuidProvider> _guidProvider;

            public PostProduct(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _commandBusMock = new Mock<ICommandBus>();
                _guidProvider = new Mock<IGuidProvider>();

                _client = builder
                    .With(commandBusMock: _commandBusMock, guidProviderMock: _guidProvider)
                    .CreateClient();
            }

            [Fact]
            public async Task ShouldReturnLocation()
            {
                var url = $"api/freddy/products";
                var productId = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E");
                _guidProvider.Setup(p => p.NewGuid()).Returns(productId);

                var response = await _client.PostObjectAsync(url, new { });

                Assert.NotNull(response.Headers.Location);
                Assert.Equal($"/{url}/{productId}", response.Headers.Location.LocalPath);
            }

            [Fact]
            public async Task ShouldExecuteAddProductCommand()
            {
                var url = $"api/freddy/products";
                var info = new ProductInfo("code", "name", "size");

                await _client.PostObjectAsync(url, info);

                _commandBusMock.Verify(b => b.Handle(It.Is(MatchingProductInfo(info))), Times.Once);
            }
            
            private Expression<Func<AddProductCommand, bool>> MatchingProductInfo(ProductInfo info)
            {
                return cmd => cmd.Info.Code == info.Code
                              && cmd.Info.Name == info.Name
                              && cmd.Info.Size == info.Size;
            }
        }

        public class DeleteProduct : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<ICommandBus> _commandBusMock;

            public DeleteProduct(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _commandBusMock = new Mock<ICommandBus>();

                _client = builder
                    .With(commandBusMock: _commandBusMock)
                    .CreateClient();
            }

            [Fact]
            public async Task ShouldReturn200()
            {
                var productId = Guid.NewGuid();
                var url = $"api/freddy/products/{productId}";

                var response = await _client.DeleteAsync(url);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task ShouldExecuteDeleteProductCommand()
            {
                var productId = Guid.NewGuid();
                var url = $"api/freddy/products/{productId}";

                await _client.DeleteAsync(url);

                _commandBusMock.Verify(c => c.Handle(It.Is<DeleteProductCommand>(cmd => cmd.ProductId == productId)),
                    Times.Once);
            }
        }

        public class PutProduct : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<ICommandBus> _commandBusMock;

            public PutProduct(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _commandBusMock = new Mock<ICommandBus>();
                
                _client = builder
                    .With(commandBusMock: _commandBusMock)
                    .CreateClient();
            }

            [Fact]
            public async Task ShouldExecuteUpdateProductCommand()
            {
                var updateProductGuid = Guid.NewGuid();
                var updateProductUrl = $"api/freddy/products/{updateProductGuid}";
                
                await _client.PutObjectAsync(updateProductUrl, new { });

                _commandBusMock.Verify(c => c.Handle(It.Is<UpdateProductCommand>(cmd => cmd.Id == updateProductGuid)),
                    Times.Once);
            }

            [Fact]
            public async Task ShouldReturn200()
            {
                var productId = Guid.NewGuid();
                var url = $"api/freddy/products/{productId}";

                var response = await _client.PutObjectAsync(url, new { });

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            
                    
            [Fact]
            public async Task WhenNotFoundExceptionThrown_ShouldReturn404()
            {
                var productId = new Guid("F9EDC8C1-00BF-454A-BF5C-58CF0E335653");
                var url = $"api/freddy/products/{productId}";
                
                _commandBusMock
                    .Setup(c => c.Handle(It.IsAny<UpdateProductCommand>()))
                    .ThrowsAsync(new NotFoundException());
                
                var response = await _client.PutObjectAsync(url, new { });
                
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}