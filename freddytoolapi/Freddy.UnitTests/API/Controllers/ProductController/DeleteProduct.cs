using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Core.Commands;
using Freddy.Application.Products.Commands.DeleteProduct;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.ProductController
{
    public static partial class ProductControllerTests
    {
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
    }
}