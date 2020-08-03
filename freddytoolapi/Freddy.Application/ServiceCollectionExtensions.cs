using Freddy.Application.Commands.Customers;
using Freddy.Application.Commands.Products;
using Freddy.Application.Commands.Products.UpdateProduct;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Customers;
using Freddy.Application.Queries.Customers.GetAllCustomers;
using Freddy.Application.Queries.Customers.GetCustomerById;
using Freddy.Application.Queries.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Freddy.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IHandleCommands<AddProductCommand>, AddProduct>();
            services.AddScoped<IHandleCommands<DeleteProductCommand>, DeleteProduct>();
            services.AddScoped<IHandleCommands<UpdateProductCommand>, UpdateProduct>();
            services.AddScoped<IHandleCommands<DeleteCustomerCommand>, DeleteCustomer>();
        }
        
        public static void AddQueries(this IServiceCollection services)
        {
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IExecuteQuery<GetAllProductsQuery, ProductView[]>, GetAllProducts>();
            services.AddScoped<IExecuteQuery<GetProductByIdQuery, ProductView>, GetProductById>();
            services.AddScoped<IExecuteQuery<GetAllCustomersQuery, CustomerView[]>, GetAllCustomers>();
            services.AddScoped<IExecuteQuery<GetCustomerByIdQuery, CustomerView>, GetCustomerById>();
        }
    }
}