using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.API.Core;
using Freddy.Application.Commands.Products;
using Freddy.Application.Core.Commands;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.ProductController
{
    public static partial class ProductControllerTests
    {
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
    }
}