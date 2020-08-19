using Freddy.API.Core;
using Freddy.Application.Core.Commands;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Customers.Commands;
using Freddy.Application.Customers.Commands.AddCustomer;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        public class PostCustomer : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<ICommandBus> _commandBusMock;
            private readonly Mock<IGuidProvider> _guidProvider;

            public PostCustomer(CommonWebApplicationBuilder<TestStartup> builder)
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
                var url = $"api/freddy/customers";
                var customerId = new Guid("71B7AD53-18FC-4E0A-99F1-54900634DCC8");

                _guidProvider.Setup(p => p.NewGuid()).Returns(customerId);
                
                var response = await _client.PostObjectAsync(url, new { });

                Assert.NotNull(response.Headers.Location);
                Assert.Equal($"/{url}/{customerId}", response.Headers.Location.LocalPath);
            }

            [Fact]
            public async Task ShouldExecuteAddCustomerCommand()
            {
                var url = $"api/freddy/customers";
                var customerInfo = new CustomerInfo("name", "email", "phone");

                await _client.PostObjectAsync(url, customerInfo);

                _commandBusMock.Verify(b => b.Handle(It.Is(MatchingCustomerInfo(customerInfo))), Times.Once);
            }

            private Expression<Func<AddCustomerCommand, bool>> MatchingCustomerInfo(CustomerInfo info)
            {
                return cmd => cmd.CustomerInfo.Email == info.Email
                              && cmd.CustomerInfo.Name == info.Name
                              && cmd.CustomerInfo.Phone == info.Phone;
            }
        }
    }
}
