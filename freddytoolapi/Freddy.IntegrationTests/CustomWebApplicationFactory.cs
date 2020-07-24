using Freddy.IntegrationTests.TestData;
using Freddy.Persistance.DbContexts;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using HostBuilder = Microsoft.Extensions.Hosting.Host;

namespace Freddy.IntegrationTests
{
    [UsedImplicitly]
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return HostBuilder
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x => x.UseStartup<TStartup>().UseTestServer());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            const string testDbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=freddydb-test;Trusted_Connection=True;";

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<DatabaseContext>));
                services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(testDbConnectionString));
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = CreateHostBuilder().Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DatabaseContext>();
                //context.Database.EnsureDeleted();
                context.Database.Migrate();
                new Products().Initialize(context);
                new Customers().Initialize(context);
            }

            host.Start();

            return host;
        }
    }
}