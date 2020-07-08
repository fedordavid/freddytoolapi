using Freddy.Application.Core.Queries;
using Freddy.Persistance.DbContexts;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;

using HostBuilder = Microsoft.Extensions.Hosting.Host;

namespace Freddy.IntegrationTests
{
    [UsedImplicitly]
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        public Mock<IQueryBus> QueryBusMock { get; } = new Mock<IQueryBus>();
        
        protected override IHostBuilder CreateHostBuilder()
        {
            return HostBuilder
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x => x.UseStartup<TStartup>().UseTestServer());
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => QueryBusMock.Object);
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = CreateHostBuilder().Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DatabaseContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
            
            host.Start();

            return host;
        }
    }
}