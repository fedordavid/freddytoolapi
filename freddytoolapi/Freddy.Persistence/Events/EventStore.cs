using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Freddy.Application.Core.Events;
using Freddy.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistence.Events
{
    public class EventStore : IEventStore
    {
        private readonly DatabaseContext _context;
        private readonly EventConverter _converter;

        public EventStore(DatabaseContext context, EventConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task<(Event[] events, int streamVersion)> GetStream(string streamName)
        {
            var eventDescriptors = await _context.Events.AsNoTracking()
                .Where(e => e.Stream == streamName)
                .OrderBy(e => e.StreamVersion)
                .ToArrayAsync();
            
            var streamVersion = eventDescriptors
                .Select(e => e.StreamVersion)
                .DefaultIfEmpty(0)
                .Max();

            var events = eventDescriptors.Select(_converter.ToEvent).ToArray();

            return (events, streamVersion);
        }

        public async Task AddToStream(string streamName, int streamVersion, IEnumerable<Event> events)
        {
            var eventDescriptors = events
                .Select((e, i) => _converter.ToDescriptor(e, streamName, i + streamVersion))
                .ToArray();
            
            await _context.Events.AddRangeAsync(eventDescriptors);
            await _context.SaveChangesAsync();
        }
    }

    public class EventConverter
    {
        private readonly Dictionary<string, Type> _eventTypes;
        
        public EventConverter(Assembly[] eventAssemblies)
        {
            _eventTypes = eventAssemblies
                .SelectMany(a => a.GetTypes().Where(t => t.IsSubclassOf(typeof(Event))))
                .ToDictionary(t => t.Name);
        }

        public EventDescriptorEntity ToDescriptor(Event @event, string streamName, int version)
        {
            var eventType = @event.GetType();

            return new EventDescriptorEntity
            {
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                Payload = JsonSerializer.Serialize(@event, eventType),
                Stream = streamName,
                Type = eventType.Name,
                StreamVersion = version
            };
        }
        
        public Event ToEvent(EventDescriptorEntity eventDescriptor)
        {
            return JsonSerializer.Deserialize(eventDescriptor.Payload, _eventTypes[eventDescriptor.Type]) as Event;
        }
    }
}