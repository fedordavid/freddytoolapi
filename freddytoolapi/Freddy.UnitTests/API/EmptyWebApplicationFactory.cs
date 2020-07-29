using Freddy.API.Controllers;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using HostBuilder = Microsoft.Extensions.Hosting.Host;

namespace Freddy.Application.UnitTests.API
{
    [UsedImplicitly]
    public class EmptyWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return HostBuilder
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x => x.UseStartup<TStartup>().UseTestServer());
        }

        public WebApplicationFactory<TStartup> WithQueryBusInstance(IQueryBus queryBus)
        {
            return WithWebHostBuilder(cfg => cfg.ConfigureServices(svc => svc.AddScoped(_ => queryBus)));
        }
        
        public WebApplicationFactory<TStartup> WithCommandBusInstance(ICommandBus commandBus)
        {
            return WithWebHostBuilder(cfg => cfg.ConfigureServices(svc => svc.AddScoped(_ => commandBus)));
        }
        
        public WebApplicationFactory<TStartup> WithQueryAndCommandBusInstance(IQueryBus queryBus, ICommandBus commandBus)
        {
            return WithWebHostBuilder(cfg => cfg.ConfigureServices(svc =>
            {
                svc.AddScoped(_ => queryBus);
                svc.AddScoped(_ => commandBus);
            }));
        }
    }
}