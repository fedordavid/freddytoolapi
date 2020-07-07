using Freddy.API.Controllers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Freddy.Application.UnitTests.API
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(OrderController).Assembly);
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}