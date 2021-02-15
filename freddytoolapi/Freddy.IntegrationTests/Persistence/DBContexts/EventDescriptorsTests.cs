using System;
using System.Threading.Tasks;
using Freddy.Host;
using Freddy.IntegrationTests.Utilities;
using Freddy.Persistence.Events;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Freddy.IntegrationTests.Persistence.DBContexts
{
    [Collection("Integration")]
    public class EventDescriptorsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly IServiceProvider _services;

        public EventDescriptorsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _services = factory.Services;
        }

        [Fact]
        public async Task ShouldAllowDifferentStreamsToHaveSameVersion()
        {
            await using var dbContext = _services.CreateDbContext();
            
            await dbContext.Events.AddAsync(new EventDescriptorEntity { Id = Guid.NewGuid(), Stream = "test-different-1", Type = "", StreamVersion = 0 });
            await dbContext.Events.AddAsync(new EventDescriptorEntity { Id = Guid.NewGuid(), Stream = "test-different-2", Type = "", StreamVersion = 0 });

            await dbContext.SaveChangesAsync();
        } 
        
        [Fact]
        public async Task ShouldNotAllowSameStreamsToHaveSameVersion()
        {
            await using var dbContext = _services.CreateDbContext();

            await dbContext.Events.AddAsync(new EventDescriptorEntity { Id = Guid.NewGuid(), Stream = "test-same", Type = "",  StreamVersion = 0 });
            await dbContext.Events.AddAsync(new EventDescriptorEntity { Id = Guid.NewGuid(), Stream = "test-same", Type = "",  StreamVersion = 0 });

            await Assert.ThrowsAsync<DbUpdateException>(() => dbContext.SaveChangesAsync());
        } 
    }
}