using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Freddy.Application.Core.Events;
using Freddy.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistence.Events
{
    public class EventStore
    {
        private readonly DatabaseContext _context;
        private readonly Dictionary<string, Type> _eventTypes;
        private readonly Dictionary<string, int> _streamVersions = new Dictionary<string, int>();

        public EventStore(DatabaseContext context)
        {
            _context = context;

            _eventTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.IsSubclassOf(typeof(Event))))
                .ToDictionary(t => t.Name);
        }

        public async Task<Event[]> GetStream(string streamName)
        {
            var eventDescriptors = await _context.Events.AsNoTracking()
                .Where(e => e.Stream == streamName)
                .OrderBy(e => e.StreamVersion)
                .ToArrayAsync();
            
            _streamVersions[streamName] = eventDescriptors
                .Select(e => e.StreamVersion)
                .DefaultIfEmpty(0)
                .Max();

            return eventDescriptors.Select(Deserialize).ToArray();
        }

        private Event Deserialize(EventDescriptorEntity eventDescriptor)
        {
            return JsonSerializer.Deserialize(eventDescriptor.Payload, _eventTypes[eventDescriptor.Type]) as Event;
        }

        public async Task AddToStream(string streamName, IEnumerable<Event> events)
        {
            var streamVersion = _streamVersions[streamName];
            
            var eventDescriptors = events.Select((e, i) => new EventDescriptorEntity
            {
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                Payload = JsonSerializer.Serialize(e, e.GetType()),
                Stream = streamName,
                Type = e.GetType().Name,
                StreamVersion = i + streamVersion + 1
            }).ToArray();

            _streamVersions[streamName] += eventDescriptors.Length;
            
            await _context.Events.AddRangeAsync(eventDescriptors);
            await _context.SaveChangesAsync();
        }
    }
}