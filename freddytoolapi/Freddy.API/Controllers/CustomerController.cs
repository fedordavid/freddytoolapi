using System;
using System.Threading.Tasks;
using Freddy.Application.Commands.Products;
using Freddy.Application.Commands.Products.UpdateProduct;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Customers;
using Freddy.Application.Queries.Customers.GetAllCustomers;
using Freddy.Application.Queries.Products;
using Microsoft.AspNetCore.Mvc;

namespace Freddy.API.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public CustomerController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet("api/freddy/customers")]
        public async Task<ActionResult<CustomerView[]>> GetAllCustomers()
        {
            return await _queryBus.Execute(new GetAllCustomersQuery());
        }
    }
}