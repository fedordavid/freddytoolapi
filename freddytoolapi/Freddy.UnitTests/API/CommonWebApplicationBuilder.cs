using Freddy.API.Controllers;
using Freddy.API.Core;
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
    public class CommonWebApplicationBuilder<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private Mock<IQueryBus> _queryBusMock;
        private Mock<ICommandBus> _commandBusMock;
        private Mock<IGuidProvider> _guidProviderMock;

        public CommonWebApplicationBuilder()
        {
            _queryBusMock = new Mock<IQueryBus>();
            _commandBusMock = new Mock<ICommandBus>();
            _guidProviderMock = new Mock<IGuidProvider>();
        }

        public CommonWebApplicationBuilder<TStartup> With(
            Mock<IQueryBus> queryBusMock = null, 
            Mock<ICommandBus> commandBusMock = null, 
            Mock<IGuidProvider> guidProviderMock = null)
        {
            var factory = new CommonWebApplicationBuilder<TStartup>
            {
                _queryBusMock = queryBusMock ?? _queryBusMock,
                _commandBusMock = commandBusMock ?? _commandBusMock,
                _guidProviderMock = guidProviderMock ?? _guidProviderMock
            };

            return factory;
        }
        
        protected override IHostBuilder CreateHostBuilder()
        {
            return HostBuilder
                .CreateDefaultBuilder()
                .ConfigureServices(svc =>
                {
                    svc.AddScoped(_ => _queryBusMock.Object);
                    svc.AddScoped(_ => _commandBusMock.Object);
                    svc.AddScoped(_ => _guidProviderMock.Object);
                })
                .ConfigureWebHostDefaults(x => x.UseStartup<TStartup>().UseTestServer());
        }
    }
}