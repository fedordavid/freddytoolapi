using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Customers.Commands.AddCustomer;
using Freddy.Application.Customers.Commands.DeleteCustomer;
using Freddy.Application.Customers.Commands.UpdateCustomer;
using Freddy.Application.Customers.Queries;
using Freddy.Application.Customers.Queries.GetAllCustomers;
using Freddy.Application.Customers.Queries.GetCustomerById;
using Freddy.Application.Orders.Commands;
using Freddy.Application.Products.Commands.AddProduct;
using Freddy.Application.Products.Commands.DeleteProduct;
using Freddy.Application.Products.Commands.UpdateProduct;
using Freddy.Application.Products.Queries;
using Freddy.Application.Products.Queries.GetAllProducts;
using Freddy.Application.Products.Queries.GetProductById;
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
            services.AddScoped<IHandleCommands<AddCustomerCommand>, AddCustomer>();
            services.AddScoped<IHandleCommands<UpdateCustomerCommand>, UpdateCustomer>();
            
            services.AddScoped<IHandleCommands<CreateOrderCommand>, CreateOrder>();
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