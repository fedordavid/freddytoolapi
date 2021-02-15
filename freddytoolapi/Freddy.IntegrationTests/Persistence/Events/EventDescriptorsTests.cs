using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Freddy.Application.Core.Events;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Freddy.Persistence.DbContexts;
using Freddy.Persistence.Events;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Freddy.IntegrationTests.Persistence.Events
{
    [Collection("Integration")]
    public class EventStoreTests : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly EventStore _store;
        private readonly DatabaseContext _dbContext;

        public EventStoreTests(CustomWebApplicationFactory<Startup> factory)
        {
            _dbContext = factory.Services.CreateDbContext();
            _store = new EventStore(_dbContext, new EventConverter(new [] { typeof(TestEvent).Assembly }));
        }

        [Fact]
        public async Task ShouldAddEventsToDb()
        {
            const string streamName = "event-store-add";
            var events = new[]
            {
                new TestEvent { Value = "Test 1" },
                new TestEvent { Value = "Test 2" }
            };

            await _store.AddToStream(streamName, 1, events);

            var storedEvents = await _dbContext.Events
                .Where(e => e.Stream == streamName).ToArrayAsync();
            
            Assert.Equal(events.Length, storedEvents.Length);
            Assert.Contains(storedEvents, e => e.Payload.Contains("Test 1"));
            Assert.Contains(storedEvents, e => e.Payload.Contains("Test 2"));
        }
        
        [Fact]
        public async Task ShouldStoreAtSpecifiedVersion()
        {
            const string streamName = "event-store-add-zero";
            var events = new[]
            {
                new TestEvent { Value = "Test 1" },
                new TestEvent { Value = "Test 2" }
            };

            await _store.AddToStream(streamName, 42, events);

            var storedEvents = await _dbContext.Events
                .Where(e => e.Stream == streamName).ToArrayAsync();
            
            Assert.Equal(42, storedEvents.Min(e => e.StreamVersion));
        }
        
        [Fact]
        public async Task ShouldHaveContinuousStreams()
        {
            const string streamName = "event-store-continuous";
            var events = new[]
            {
                new TestEvent { Value = "Test 1" },
                new TestEvent { Value = "Test 2" },
                new TestEvent { Value = "Test 3" }
            };

            await _store.AddToStream(streamName, 1, events);

            var storedEvents = await _dbContext.Events
                .Where(e => e.Stream == streamName).ToArrayAsync();
            
            Assert.Equal(1, storedEvents.FirstOrDefault(e => e.Payload.Contains("Test 1"))?.StreamVersion);
            Assert.Equal(2, storedEvents.FirstOrDefault(e => e.Payload.Contains("Test 2"))?.StreamVersion);
            Assert.Equal(3, storedEvents.FirstOrDefault(e => e.Payload.Contains("Test 3"))?.StreamVersion);
        }
        
        [Fact]
        public async Task ShouldReturnEventsFromDb()
        {
            const string streamName = "event-store-get";
            
            var eventDescriptors = new[]
            {
                new TestEvent { Value = "Test 1" }.GetDescriptor(streamName, 1),
                new TestEvent { Value = "Test 2" }.GetDescriptor(streamName, 2),
                new TestEvent { Value = "Test 3" }.GetDescriptor(streamName, 3)
            };

            await _dbContext.Events.AddRangeAsync(eventDescriptors);
            await _dbContext.SaveChangesAsync();
            
            var events = (await _store.GetStream(streamName)).events;

            Assert.Equal(eventDescriptors.Length, events.Length);
            
            var testEvents = events.Select(Assert.IsType<TestEvent>).ToArray();
            
            Assert.Equal("Test 1", testEvents[0].Value);
            Assert.Equal("Test 2", testEvents[1].Value);
            Assert.Equal("Test 3", testEvents[2].Value);
        }
        
        [Fact]
        public async Task ShouldReturnCorrectStreamVersion()
        {
            const string streamName = "event-store-get-version";
            
            var eventDescriptors = new[]
            {
                new TestEvent { Value = "Test 1" }.GetDescriptor(streamName, 77)
            };

            await _dbContext.Events.AddRangeAsync(eventDescriptors);
            await _dbContext.SaveChangesAsync();
            
            var streamVersion = (await _store.GetStream(streamName)).streamVersion;

            Assert.Equal(77, streamVersion);
        }
        
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }

    public class TestEvent : Event
    {
        public string Value { get; set; }

        public EventDescriptorEntity GetDescriptor(string streamName, int version)
        {
            return new EventDescriptorEntity
            {
                Stream = streamName,
                StreamVersion = version,
                Id = Guid.NewGuid(),
                Payload = JsonSerializer.Serialize(this),
                Type = nameof(TestEvent)
            };
        }
    }
}