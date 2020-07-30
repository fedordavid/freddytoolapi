using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Commands.Products.UpdateProduct;
using Freddy.Application.Core.Commands;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.ProductController
{
    public static partial class ProductControllerTests
    {
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