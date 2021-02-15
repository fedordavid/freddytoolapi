using System.Collections.Generic;
using System.Threading.Tasks;
using Freddy.Application.Core.Events;

namespace Freddy.Persistence.Events
{
    public interface IEventStore
    {
        Task<(Event[] events, int streamVersion)> GetStream(string streamName);
        Task AddToStream(string streamName, int streamVersion, IEnumerable<Event> events);
    }
}