using System;
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
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Freddytool API",
                Description = "Freddytool API serves only learning purposes",
                Contact = new OpenApiContact
                {
                    Name = "David Fedor",
                    Email = "dark1500@gmail.com",
                }
            }));
        }

        [UsedImplicitly] // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors(cfg => cfg
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .WithExposedHeaders(HeaderNames.Location));

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
