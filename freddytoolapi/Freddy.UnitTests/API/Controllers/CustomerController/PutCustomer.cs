using Freddy.Application.Commands.Customers.UpdateCustomer;
using Freddy.Application.Core.Commands;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        public class PutCustomer : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<ICommandBus> _commandBusMock;
            

            public PutCustomer(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _commandBusMock = new Mock<ICommandBus>();
                _client = builder
                    .With(commandBusMock: _commandBusMock)
                    .CreateClient();
            }

            [Fact]
            public async Task ShouldExecuteUpdateCustomerCommand()
            {
                var customerId = Guid.NewGuid();
                var url = $"api/freddy/customers/{customerId}";
                
                await _client.PutObjectAsync(url, new { });

                _commandBusMock.Verify(b => b.Handle(It.Is<UpdateCustomerCommand>(cmd => cmd.CustomerId == customerId)));
            }

            [Fact]
            public async Task ShouldReturn200()
            {
                var customerId = Guid.NewGuid();
                var url = $"api/freddy/customers/{customerId}";

                var response = await _client.PutObjectAsync(url, new { });

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

    }
}
