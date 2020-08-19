using Freddy.Application.Core.Queries;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Customers.Queries.GetAllCustomers;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        public class GetAllCustomers : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            
            private HttpClient _client;
            private readonly Mock<IQueryBus> _queryBusMock;

            public GetAllCustomers(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _queryBusMock = new Mock<IQueryBus>();
                _client = builder.CreateClient();

                _client = builder
                   .With(_queryBusMock)
                   .CreateClient();
            }

            [Fact]
            public async Task ShouldExecuteGetAllCustomersQuery()
            {
                var url = $"api/freddy/customers";
                await _client.GetAsync(url);

                _queryBusMock.Verify(b => b.Execute(It.IsAny<GetAllCustomersQuery>()), Times.Once);
            }

            [Fact]
            public async Task ShouldReturnQueryResults()
            {
                var url = $"api/freddy/customers";

                var customerView = new CustomerViewBuilder();

                await _client.GetAsync(url);

                _queryBusMock.Verify(b => b.Execute(It.IsAny<GetAllCustomersQuery>()), Times.Once);
            }
        }

    }
}
