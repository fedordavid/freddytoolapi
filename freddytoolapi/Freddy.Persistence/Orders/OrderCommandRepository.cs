using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freddy.Application.Orders.Commands;
using Freddy.Persistence.Events;

namespace Freddy.Persistence.Orders
{
    public class OrderCommandRepository : IOrders
    {
        private const int InitialStreamVersion = 1;
        
        private readonly IEventStore _eventStore;
        
        private readonly Dictionary<Guid, int> _streamVersions = new Dictionary<Guid, int>();

        public OrderCommandRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Order> Get(Guid orderId)
        {
            var (events, version) = await _eventStore.GetStream(GetStreamName(orderId));

            _streamVersions[orderId] = version + 1;

            return Order.Initialize(events);
        }

        public Task Publish(IEnumerable<OrderEvent> eventsEnumerable)
        {
            var events = eventsEnumerable.ToArray();

            var orderId = events.Select(e => e.OrderId)
                .Distinct()
                .Single();

            var streamName = GetStreamName(orderId);

            var streamVersion = _streamVersions.TryGetValue(orderId, out var version) ? version : InitialStreamVersion;

            _streamVersions[orderId] = streamVersion + events.Length;
            
            return _eventStore.AddToStream(streamName, streamVersion, events);
        }

        private string GetStreamName(Guid orderId) => $"{nameof(Order)}/{orderId}";
    }
}