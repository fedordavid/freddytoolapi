using System;
using System.Threading.Tasks;
using Freddy.API.Core;
using Freddy.Application.Commands.Customers;
using Freddy.Application.Commands.Customers.AddCustomer;
using Freddy.Application.Commands.Customers.UpdateCustomer;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Customers;
using Freddy.Application.Queries.Customers.GetAllCustomers;
using Freddy.Application.Queries.Customers.GetCustomerById;
using Microsoft.AspNetCore.Mvc;

namespace Freddy.API.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        private readonly IGuidProvider _guidProvider;

        public CustomerController(IQueryBus queryBus, ICommandBus commandBus, IGuidProvider guidProvider)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
            _guidProvider = guidProvider;
        }

        [HttpGet("api/freddy/customers")]
        public async Task<ActionResult<CustomerView[]>> GetAllCustomers()
        {
            return await _queryBus.Execute(new GetAllCustomersQuery());
        }

        [HttpGet("api/freddy/customers/{customerId}")]
        public async Task<CustomerView> GetCustomerById(Guid customerId)
        {
            return await _queryBus.Execute(new GetCustomerByIdQuery(customerId));
        }

        [HttpDelete("api/freddy/customers/{customerId}")]
        public async Task DeleteCustomer(Guid customerId)
        {
            await _commandBus.Handle(new DeleteCustomerCommand(customerId));
        }

        [HttpPost("api/freddy/customers")]
        public async Task<ActionResult> PostCustomer(CustomerInfo info)
        {
            var customerId = _guidProvider.NewGuid();
            await _commandBus.Handle(new AddCustomerCommand(customerId, info));
            return CreatedAtAction(nameof(GetCustomerById), new { customerId }, null);
        }

        [HttpPut("api/freddy/customers/{customerId}")]
        public async Task<ActionResult> PutCustomer(Guid customerId, CustomerInfo customerInfo)
        {
            await _commandBus.Handle(new UpdateCustomerCommand(customerId, customerInfo));
            return Ok();
        }
    }
}