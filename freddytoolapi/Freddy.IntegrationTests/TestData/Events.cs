using System.Collections.Generic;
using Freddy.Persistence.DbContexts;
using Freddy.Persistence.Events;

namespace Freddy.IntegrationTests.TestData
{
    internal sealed class Events
    {
        public void Initialize(DatabaseContext context)
        {
            context.Events.RemoveRange(context.Events);

            context.Events.AddRange(SeedData());

            context.SaveChanges();
        }

        public IEnumerable<EventDescriptorEntity> SeedData()
        {
            yield break;
        }
    }
}