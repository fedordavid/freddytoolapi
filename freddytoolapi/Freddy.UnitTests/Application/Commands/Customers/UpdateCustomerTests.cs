using Freddy.Application.Core.Commands;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using System;
using System.Threading.Tasks;
using Freddy.Application.Customers.Commands;
using Freddy.Application.Customers.Commands.UpdateCustomer;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Commands.Customers
{
    public class UpdateCustomerTests 
    {
        private readonly Mock<ICustomers> _customersMock;
        private readonly UpdateCustomer _updateCustomer;

        public UpdateCustomerTests()
        {
            _customersMock = new Mock<ICustomers>();
            _updateCustomer = new UpdateCustomer(_customersMock.Object);
        }

        [Fact]
        public async Task ShouldExecutePutCustomerCommand()
        {
            var customerId = new Guid("A9900898-9A71-4315-83F1-A0636FD2DC3A");
            var customerInfo = new CustomerInfo("change-name-David", "change-email@gmail.com", "change-phone-0903000000");

            _customersMock.Setup(s => s.Get(customerId)).ReturnsAsync(new Customer(customerId, new CustomerInfo()));

            await _updateCustomer.Handle(new UpdateCustomerCommand(customerId, customerInfo));

            _customersMock.Verify(customers => customers.Update(It.Is(A.Customer.With(customerId, customerInfo))));
        }

        [Fact]
        public async Task Execute_ShouldGetCustomer()
        {
            var customerId = new Guid("5E6325E1-F355-403F-848F-1A916AC4F353");
            var customerInfo = new CustomerInfo("change-name-David", "change-email@gmail.com", "change-phone-0903000000");

            _customersMock.Setup(s => s.Get(customerId)).ReturnsAsync(new Customer(customerId, new CustomerInfo()));

            await _updateCustomer.Handle(new UpdateCustomerCommand(customerId, customerInfo));

            _customersMock.Verify(customers => customers.Get(customerId), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldThrowExceptionOnNotExistingCustomer()
        {
            var customerId = new Guid("F3D8BDC4-9BD3-49FC-8428-5D101243C95F");
            var customerInfo = new CustomerInfo("change-name-David", "change-email@gmail.com", "change-phone-0903000000");

            _customersMock.Setup(s => s.Get(customerId)).ReturnsAsync((Customer)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _updateCustomer.Handle(new UpdateCustomerCommand(customerId, customerInfo)));
        }
    }
}
