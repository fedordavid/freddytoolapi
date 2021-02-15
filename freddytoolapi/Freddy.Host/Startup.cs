using Freddy.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Freddy.API;
using Freddy.Application;
using JetBrains.Annotations;
using Microsoft.Net.Http.Headers;

namespace Freddy.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApi();
            services.AddQueries();
            services.AddCommands();
            services.AddPersistence(_configuration);
        }

        [UsedImplicitly] // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(cfg => cfg
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .WithExposedHeaders(HeaderNames.Location));
            }
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
