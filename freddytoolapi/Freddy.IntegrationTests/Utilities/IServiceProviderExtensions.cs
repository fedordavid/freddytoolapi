using Freddy.Persistance.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Freddy.IntegrationTests.Utilities
{
    public static class CreateDatabaseContextExtension
    {
        public static DatabaseContext CreateDbContext(this IServiceProvider services)
        {
            return services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
        }
    }
}
