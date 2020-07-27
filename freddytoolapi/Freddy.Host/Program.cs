using Freddy.Persistance;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
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
            
            PersistenceInitializer.Initialize(host.Services);
            
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
