using System;
using System.Threading.Tasks;
using Freddy.Application.Commands.Products;
using Freddy.Application.Commands.Products.UpdateProduct;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Queries.Products;
using Microsoft.AspNetCore.Mvc;

namespace Freddy.API.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;

        public OrderController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet("api/freddy/products/{productId}")]
        public async Task<ActionResult<ProductView>> GetProductById(Guid productId)
        {
            return await _queryBus.Execute(new GetProductByIdQuery(productId));
        }

        [HttpGet("api/freddy/products")]
        public async Task<ActionResult<ProductView[]>> GetAllProducts()
        {
            return await _queryBus.Execute(new GetAllProductsQuery());
        }
        
        [HttpPost("api/freddy/products")]
        public async Task<ActionResult> PostProduct(ProductInfo productInfo)
        {
            var productId = Guid.NewGuid();
            await _commandBus.Handle(new AddProductCommand(productId, productInfo));
            return CreatedAtAction(nameof(GetProductById), new {productId}, null);
        }

        [HttpDelete("api/freddy/products/{productId}")]
        public async Task<ActionResult> DeleteProduct(Guid productId)
        {
            await _commandBus.Handle(new DeleteProductCommand(productId));
            return Ok();
        }

        [HttpPut("api/freddy/products/{productId}")]
        public async Task<ActionResult> UpdateProduct(Guid productId, ProductInfo productInfo)
        {
            await _commandBus.Handle(new UpdateProductCommand(productId, productInfo));
            return Ok();
        }
    }
}