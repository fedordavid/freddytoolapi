using System.Text.Json;
using Freddy.Application.Core.Events;
using Freddy.Persistence.Events;
using Xunit;

namespace Freddy.Application.UnitTests.Persistence.Events
{
    public class EventConverterTests
    {
        private EventConverter _converter;

        public EventConverterTests()
        {
            _converter = new EventConverter(new [] { typeof(TestEvent).Assembly });
        }

        [Fact]
        public void ShouldConvertDescriptorToEvent()
        {
            var @event = new TestEvent { Value = "Test Value" };
            
            var descriptor = new EventDescriptorEntity
            {
                Type = nameof(TestEvent),
                Payload = JsonSerializer.Serialize(@event)
            };

            var deserialized = _converter.ToEvent(descriptor);

            var testEvent = Assert.IsType<TestEvent>(deserialized);
            Assert.Equal(@event.Value, testEvent.Value);
        }
        
        [Fact]
        public void ShouldBeAbleToConvertDataToDescriptor()
        {
            var @event = new TestEvent { Value = "Test Value" };

            var descriptor = _converter.ToDescriptor(@event, "test-stream", 42);

            Assert.Equal(JsonSerializer.Serialize(@event), descriptor.Payload);
            Assert.Equal("test-stream", descriptor.Stream);
            Assert.Equal(nameof(TestEvent), descriptor.Type);
            Assert.Equal(42, descriptor.StreamVersion);
        }
    }

    public class TestEvent: Event
    {
        public string Value { get; set; }
    }
}