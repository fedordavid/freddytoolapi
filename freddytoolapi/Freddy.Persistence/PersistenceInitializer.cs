using System;
using Freddy.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Freddy.Persistence
{
    public static class PersistenceInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            
            var context = scope.ServiceProvider.GetService<DatabaseContext>();
            
            //context.Database.EnsureCreated();
            
            //context.Database.Migrate();
        }
    }
}