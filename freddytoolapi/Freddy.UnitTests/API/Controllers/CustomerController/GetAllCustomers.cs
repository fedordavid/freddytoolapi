using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Customers;
using Freddy.Application.Queries.Customers.GetAllCustomers;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
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
                var response = await _client.GetAsync(url);

                _queryBusMock.Verify(b => b.Execute(It.IsAny<GetAllCustomersQuery>()), Times.Once);
            }

            [Fact]
            public async Task ShouldReturnQueryResults()
            {
                var url = $"api/freddy/customers";

                var customerView = new CustomerViewBuilder();

                var customers = new CustomerView[] 
                {
                    customerView.WithId("2BD97FFF-61AE-4CE4-B638-BE7289841740"),
                    customerView.WithId("BAC45398-8F89-46E6-B80F-5BA17BF2235E")
                };

                var response = await _client.GetAsync(url);

                _queryBusMock.Verify(b => b.Execute(It.IsAny<GetAllCustomersQuery>()), Times.Once);
            }
        }

    }
}
