using System;
using System.Threading.Tasks;
using Freddy.API.Core;
using Freddy.Application.Core.Commands;
using Freddy.Application.Core.Queries;
using Freddy.Application.Products.Commands;
using Freddy.Application.Products.Commands.AddProduct;
using Freddy.Application.Products.Commands.DeleteProduct;
using Freddy.Application.Products.Commands.UpdateProduct;
using Freddy.Application.Products.Queries;
using Freddy.Application.Products.Queries.GetAllProducts;
using Freddy.Application.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Mvc;

namespace Freddy.API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        private readonly IGuidProvider _guidProvider;

        public ProductController(IQueryBus queryBus, ICommandBus commandBus, IGuidProvider guidProvider)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
            _guidProvider = guidProvider;
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
            var productId = _guidProvider.NewGuid();
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