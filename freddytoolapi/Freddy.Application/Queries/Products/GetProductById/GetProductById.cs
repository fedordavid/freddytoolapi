using System.Threading.Tasks;
using Freddy.Application.Core.Queries;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Application.Queries.Products
{
    public class GetProductById : IExecuteQuery<GetProductByIdQuery, ProductView>
    {
        private readonly IProductViews _products;

        public GetProductById(IProductViews products)
        {
            _products = products;
        }

        public Task<ProductView> Execute(GetProductByIdQuery query)
        {
            return _products.Products.FirstOrDefaultAsync(p => p.Id == query.ProductId);
        }
    }
}
