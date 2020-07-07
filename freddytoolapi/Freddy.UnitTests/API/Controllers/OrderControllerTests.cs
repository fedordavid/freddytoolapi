using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Freddy.Application.Commands.Products;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Products;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers
{
    public class OrderControllerTests : IClassFixture<EmptyWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient _client;

        private readonly Mock<IQueryBus> _queryBusMock;
        private readonly Mock<ICommandBus> _commandBusMock;

        public OrderControllerTests(EmptyWebApplicationFactory<TestStartup> factory)
        {
            _queryBusMock = new Mock<IQueryBus>();
            _commandBusMock = new Mock<ICommandBus>();
            
            _client = factory
                .WithQueryAndCommandBusInstance(_queryBusMock.Object, _commandBusMock.Object)
                .CreateClient();
        }

        [Fact]
        public async Task GetProductById_ShouldExecuteGetProductByIdQuery()
        {
            var productId = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E");
            var url = $"api/freddy/products/{productId}";

            await _client.GetAsync(url);
            
            _queryBusMock.Verify(b => b.Execute(It.Is<GetProductByIdQuery>(q => q.ProductId == productId)), Times.Once);
        }
        
        [Fact]
        public async Task GetProductById_ShouldReturnQueryResult()
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
        
        [Fact]
        public async Task GetAllProducts_ShouldExecuteGetProductByIdQuery()
        {
            var url = $"api/freddy/products";
            
            await _client.GetAsync(url);
            
            _queryBusMock.Verify(b => b.Execute(It.IsAny<GetAllProductsQuery>()), Times.Once);
        }
        
        [Fact]
        public async Task GetAllProducts_ShouldReturnQueryResult()
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

        [Fact]
        public async Task PostProduct_ShouldReturnLocation()
        {
            var url = $"api/freddy/products";
            
            var response = await _client.PostObjectAsync(url, new {});
            
            Assert.NotEmpty(response.Headers.Location.LocalPath);
            Assert.NotNull(response.Headers.Location.LocalPath);
        }

        [Fact]
        public async Task PostProduct_ShouldExecuteAddProductCommand()
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
}