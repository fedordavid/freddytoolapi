using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Freddy.Application.Commands.Customers;
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
    public class CustomerControllerTests : IClassFixture<EmptyWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient _client;

        private readonly Mock<IQueryBus> _queryBusMock;
        private readonly Mock<ICommandBus> _commandBusMock;

        public CustomerControllerTests(EmptyWebApplicationFactory<TestStartup> factory)
        {
            _queryBusMock = new Mock<IQueryBus>();
            _commandBusMock = new Mock<ICommandBus>();
            
            _client = factory
                .WithQueryAndCommandBusInstance(_queryBusMock.Object, _commandBusMock.Object)
                .CreateClient();
        }

        //TODO: Tests for GetCustomers

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

            var response = await _client.DeleteAsync(url);

            _commandBusMock.Verify(c => c.Handle(It.Is<DeleteCustomerCommand>(cmd => cmd.CustomerId == customerId)), Times.Once);
        }
    }
}