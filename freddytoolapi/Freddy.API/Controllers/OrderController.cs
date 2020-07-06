using AutoMapper;
using Freddy.Application;
using Freddy.Application.Queries;
using Freddy.Application.Queries.Product.GetProduct;
using Microsoft.AspNetCore.Mvc;

namespace freddytoolapi.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IQueryService _queryService;

        public OrderController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet("api/freddy/product/{productId}")]
        public ActionResult GetProduct(int productId)
        {
            var result = _queryService.GetProduct(productId);
            //var result_two = ExecuteQuery(new GetProductQuery(productId)); --> ma to 
            return Ok(result);
        }

        [HttpGet("api/freddy/products")]
        public ActionResult GetProducts()
        {
            var result = _queryService.GetProducts();
            return Ok(result);
        }
    }
}