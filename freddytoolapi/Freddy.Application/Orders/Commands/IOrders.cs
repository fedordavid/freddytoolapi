using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Freddy.Application.Core.Events;

namespace Freddy.Application.Orders.Commands
{
    public interface IOrders
    {
        Task<Order> Get(Guid orderId);
        
        Task Publish(IEnumerable<OrderEvent> eventsEnumerable);
    }
}