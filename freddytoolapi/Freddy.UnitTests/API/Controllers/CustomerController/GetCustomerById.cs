using Freddy.Application.Core.Queries;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Freddy.Application.Customers.Queries;
using Freddy.Application.Customers.Queries.GetCustomerById;
using Xunit;

namespace Freddy.Application.UnitTests.API.Controllers.CustomerController
{
    public static partial class CustomerControllerTests
    {
        public class GetCustomerById : IClassFixture<CommonWebApplicationBuilder<TestStartup>>
        {
            private readonly HttpClient _client;
            private readonly Mock<IQueryBus> _queryBusMock;

            public GetCustomerById(CommonWebApplicationBuilder<TestStartup> builder)
            {
                _queryBusMock = new Mock<IQueryBus>();
                _client = builder
                    .With(_queryBusMock)
                    .CreateClient();
            }

            [Fact]
            public async Task ShouldReturn200()
            {
                var customerId = new Guid("7A23DC65-C014-4A67-826E-7998341B38D9");
                var url = $"api/freddy/customers/{customerId}";

                _queryBusMock.Setup(m => m.Execute(It.IsAny<Query<CustomerView>>())).ReturnsAsync(new CustomerView());

                var response = await _client.GetAsync(url);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            [Fact]
            public async Task Execute_ShouldExecuteGetCustomerByIdQuery()
            {
                var customerId = new Guid("DA1FBD1B-3CC7-4639-B76C-E983CBDBB564");
                var url = $"api/freddy/customers/{customerId}";

                var response = await _client.GetAsync(url);

                _queryBusMock.Verify(m => m.Execute(It.Is<GetCustomerByIdQuery>(q => q.CustomerId == customerId)), Times.Once);
            }

            [Fact]
            public async Task ShouldReturnQueryResult()
            {
                var customer = new CustomerView { Id = new Guid("46F30933-1501-49DC-A356-F8A7163694AE") };
                var url = $"api/freddy/customers/{customer.Id}";

                _queryBusMock
                    .Setup(q => q.Execute(It.IsAny<Query<CustomerView>>()))
                    .ReturnsAsync(customer);

                var result = await _client.GetObjectAsync<CustomerView>(url);

                Assert.NotNull(result);
                Assert.Equal(customer.Id, result.Id);
            }
        }
    }
}

