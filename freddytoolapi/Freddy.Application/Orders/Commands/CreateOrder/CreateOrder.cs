using System;
using System.Threading.Tasks;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Orders.Commands
{
    public class CreateOrder : IHandleCommands<CreateOrderCommand>
    {
        private readonly IOrders _orders;

        public CreateOrder(IOrders orders)
        {
            _orders = orders;
        }

        public Task Handle(CreateOrderCommand command)
        {
            var customer = new OrderCustomer(command.CustomerId);
            var events = customer.CreateOrder(command.OrderId);
            return _orders.Publish(events);
        }
    }

    public class CreateOrderCommand : Command
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public CreateOrderCommand(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
