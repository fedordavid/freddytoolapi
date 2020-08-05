using Freddy.Application.Commands.Customers;
using Freddy.Application.Commands.Customers.AddCustomer;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Commands.Customers
{

    public class AddCustomerTests
    {
        private readonly Mock<ICustomers> _customersMock;
        private AddCustomer _addCustomer;

        public AddCustomerTests()
        {
            _customersMock = new Mock<ICustomers>();
            _addCustomer = new AddCustomer(_customersMock.Object);
        }

        [Fact]
        public void Execute_ShouldAddCustomerBasedOnCommandParameters()
        {
            var url = $"api/freddy/customers";
            var customerId = new Guid("1484E926-E36E-4CEF-B897-281AF4005E4F");
            var customerInfo = new CustomerInfo("david", "em@il.com", "+421903466860");

            _addCustomer.Handle(new AddCustomerCommand(customerId, customerInfo));

            _customersMock.Verify(c => c.Add(It.Is(A.Customer.With(customerId, customerInfo))), Times.Once);
        }
    }
}
