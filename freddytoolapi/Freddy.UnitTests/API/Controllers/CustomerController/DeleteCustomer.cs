using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Customers.Commands.DeleteCustomer;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.ProductController
{
    public static partial class CustomerControllerTests
    {
        public class DeleteCustomer : IClassFixture<CustomerControllerTests<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<IQueryBus> _queryBusMock;
            private readonly Mock<ICommandBus> _commandBusMock;

            public DeleteCustomer(CustomerControllerTests<TestStartup> factory)
            {
                _queryBusMock = new Mock<IQueryBus>();
                _commandBusMock = new Mock<ICommandBus>();

                _client = factory
                    .WithQueryAndCommandBusInstance(_queryBusMock.Object, _commandBusMock.Object)
                    .CreateClient();
            }

            [Fact]
            public async Task DeleteCustomer_ShouldReturn200()
            {
                var customerId = Guid.NewGuid();
                var url = $"api/freddy/customers/{customerId}";

                var response = await _client.DeleteAsync(url);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task DeleteCustomer_ShouldExecuteDeleteCustomerCommand()
            {
                var customerId = Guid.NewGuid();
                var url = $"api/freddy/customers/{customerId}";

                await _client.DeleteAsync(url);

                _commandBusMock.Verify(c => c.Handle(It.Is<DeleteCustomerCommand>(cmd => cmd.CustomerId == customerId)), Times.Once);
            }
        }
    }
}