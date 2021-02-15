using System;
using AutoMapper;
using Freddy.Application.Customers.Commands;
using Freddy.Application.Customers.Queries;
using Freddy.Application.Orders.Commands;
using Freddy.Application.Products.Commands;
using Freddy.Application.Products.Queries;
using Freddy.Persistence.Customers;
using Freddy.Persistence.DbContexts;
using Freddy.Persistence.Events;
using Freddy.Persistence.Orders;
using Freddy.Persistence.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Freddy.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ProductViewProfile));
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped<IProductViews, ProductQueryRepository>();
            services.AddScoped<ICustomerViews, CustomerQueryRepository>();
            services.AddScoped<IProducts, ProductCommandRepository>();
            services.AddScoped<ICustomers, CustomerCommandRepository>();
            services.AddScoped<IOrders, OrderCommandRepository>();

            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped(_ => new EventConverter(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}