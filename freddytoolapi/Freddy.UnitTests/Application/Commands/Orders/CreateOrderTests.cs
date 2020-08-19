using System;
using Freddy.Application.Orders.Commands;
using Freddy.Application.UnitTests.Utilities;
using Moq;
using Xunit;

namespace Freddy.Application.UnitTests.Application.Commands.Orders
{
    public class CreateOrderTests
    {
        private readonly Mock<IOrders> _ordersMock;
        private readonly CreateOrder _createOrder;

        public CreateOrderTests()
        {
            _ordersMock = new Mock<IOrders>();
            _createOrder = new CreateOrder(_ordersMock.Object);
        }

        [Fact]
        public void Execute_ShouldAddCustomerBasedOnCommandParameters()
        {
            var customerId = new Guid("1484E926-E36E-4CEF-B897-281AF4005E4F");
            var orderId = new Guid("47E60661-7DE9-4633-91CF-2A5EB80AA4CB");

            _createOrder.Handle(new CreateOrderCommand(orderId, customerId));

            _ordersMock.Verify(c => c.Publish(It.Is(An.OrderCreated.With(orderId, customerId))), Times.Once);
        }
    }
}