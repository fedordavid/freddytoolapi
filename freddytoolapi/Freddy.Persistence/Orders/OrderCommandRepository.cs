using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Freddy.Application.Core.Events;
using Freddy.Application.Orders.Commands;

namespace Freddy.Persistence.Orders
{
    public class OrderCommandRepository : IOrders
    {
        public Task<Order> Get(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task Publish(IEnumerable<Event> events)
        {
            throw new NotImplementedException();
        }
    }
}