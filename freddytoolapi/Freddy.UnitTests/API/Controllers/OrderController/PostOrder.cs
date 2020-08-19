using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.API.Core;
using Freddy.Application.Core.Commands;
using Freddy.Application.Orders.Commands;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.OrderController
{
    public partial class OrderControllerTests
    {
        public class PostOrder : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<ICommandBus> _commandBusMock;
            private readonly Mock<IGuidProvider> _guidProvider;

            public PostOrder(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _commandBusMock = new Mock<ICommandBus>();
                _guidProvider = new Mock<IGuidProvider>();

                _client = builder
                    .With(commandBusMock: _commandBusMock, guidProviderMock: _guidProvider)
                    .CreateClient();
            }

            [Fact]
            public async Task ShouldExecuteAddCustomerCommand()
            {
                var customerId = new Guid("30CEB400-4810-4874-8CD8-A7DBBD12ACCD");
                var orderId = new Guid("AD582460-996E-45CA-AE6D-63DC5BB742F1");
                var url = $"api/freddy/customers/{customerId}/orders";
                _guidProvider.Setup(p => p.NewGuid()).Returns(orderId);

                await _client.PostObjectAsync(url, new {});

                _commandBusMock.Verify(b => b.Handle(It.Is(Matching(orderId, customerId))), Times.Once);
            }

            [Fact]
            public async Task ShouldReturnLocation()
            {
                var customerId = new Guid("30CEB400-4810-4874-8CD8-A7DBBD12ACCD");
                var orderId = new Guid("AD582460-996E-45CA-AE6D-63DC5BB742F1");
                var url = $"api/freddy/customers/{customerId}/orders";
                _guidProvider.Setup(p => p.NewGuid()).Returns(orderId);
                
                var response = await _client.PostObjectAsync(url, new {});
                
                Assert.NotNull(response.Headers.Location);
                Assert.Equal($"/{url}/{orderId}", response.Headers.Location.LocalPath);
            }

            private Expression<Func<CreateOrderCommand, bool>> Matching(Guid orderId, Guid customerId)
            {
                return cmd => cmd.OrderId == orderId && cmd.CustomerId == customerId;
            }
        }
    }
}