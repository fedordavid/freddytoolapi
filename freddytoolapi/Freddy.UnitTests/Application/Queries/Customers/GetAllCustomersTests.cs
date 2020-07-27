using Freddy.Application.Queries;
using Freddy.Application.Queries.Customers;
using Freddy.Application.Queries.Customers.GetAllCustomers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Queries.Customers
{
    public class GetAllCustomersTests
    {
        private readonly Mock<ICustomerViews> _customersViewsMock;
        private readonly GetAllCustomers _getAllCustomers;

        public GetAllCustomersTests()
        {
            _customersViewsMock = new Mock<ICustomerViews>();
            _getAllCustomers = new GetAllCustomers(_customersViewsMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCallGetAllCustomers()
        {
            _customersViewsMock.SetupGet(c => c.Customers).Returns(new ViewCollectionMock<CustomerView>());
            await _getAllCustomers.Execute(new GetAllCustomersQuery());
            _customersViewsMock.Verify(v => v.Customers);
        }

        [Fact]
        public async Task Execute_ShouldReturnAllViews()
        {
            var customers = new[]
            {
                new CustomerView {Id = new Guid("5412D947-05DB-45AE-AA20-6ADA289FBA0E")},
                new CustomerView {Id = new Guid("A017EFE7-D230-42BD-84F8-65574DD5AF6E")}
            };

            _customersViewsMock.SetupGet(c => c.Customers).Returns(new ViewCollectionMock<CustomerView>(customers));
            var result = await _getAllCustomers.Execute(new GetAllCustomersQuery());
            Assert.Equal(customers, result);
        }
    }
}
