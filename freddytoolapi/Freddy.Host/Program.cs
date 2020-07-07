using Freddy.Persistance.DbContexts;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HostBuilder = Microsoft.Extensions.Hosting.Host;

namespace Freddy.Host
{
    [UsedImplicitly]
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DatabaseContext>();
                // context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
            
            host.Run();
        }

        [UsedImplicitly]
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return HostBuilder
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}
