using System;
using Freddy.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Freddy.Persistance
{
    public static class PersistenceInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            
            var context = scope.ServiceProvider.GetService<DatabaseContext>();
            
            //context.Database.EnsureDeleted();
            
            context.Database.Migrate();
        }
    }
}