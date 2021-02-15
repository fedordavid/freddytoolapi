using System.Threading.Tasks;
using Freddy.Application.Core.Queries;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Application.Products.Queries.GetAllProducts
{
    public class GetAllProducts : IExecuteQuery<GetAllProductsQuery, ProductView[]>
    {
        private readonly IProductViews _products;

        public GetAllProducts(IProductViews products)
        {
            _products = products;
        }

        public Task<ProductView[]> Execute(GetAllProductsQuery query)
        {
            return _products.Products.ToArrayAsync();
        }
    }
}