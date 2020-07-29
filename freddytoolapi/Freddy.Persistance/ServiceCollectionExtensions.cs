using AutoMapper;
using Freddy.Application.Commands.Customers;
using Freddy.Application.Commands.Products;
using Freddy.Application.Queries;
using Freddy.Application.Queries.Products;
using Freddy.Persistance.Customers;
using Freddy.Persistance.DbContexts;
using Freddy.Persistance.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Freddy.Persistance
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
        }
    }
}