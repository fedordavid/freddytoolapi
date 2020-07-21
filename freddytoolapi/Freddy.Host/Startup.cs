using AutoMapper;
using Freddy.Persistance;
using Freddy.Persistance.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Freddy.Application.Commands.Products;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Products;
using JetBrains.Annotations;
using Freddy.Application.Commands.Products.UpdateProduct;
using Freddy.Application.Queries.Customers;
using Freddy.Application.Queries.Customers.GetAllCustomers;
using Freddy.Application.Queries;

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
            services.AddControllers();
            services.AddAutoMapper(typeof(ProductViewProfile));
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(_configuration.GetConnectionString("Database")));

            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IExecuteQuery<GetAllProductsQuery, ProductView[]>, GetAllProducts>();
            services.AddScoped<IExecuteQuery<GetProductByIdQuery, ProductView>, GetProductById>();
            services.AddScoped<IExecuteQuery<GetAllCustomersQuery, CustomerView[]>, GetAllCustomers>();

            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IHandleCommands<AddProductCommand>, AddProduct>();
            services.AddScoped<IHandleCommands<DeleteProductCommand>, DeleteProduct>();
            services.AddScoped<IHandleCommands<UpdateProductCommand>, UpdateProduct>();

            services.AddScoped<IProductViews, ProductQueryRepository>();
            services.AddScoped<ICustomerViews, CustomerQueryRepository>();
            services.AddScoped<IProducts, ProductCommandRepository>();
        }

        [UsedImplicitly] // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
