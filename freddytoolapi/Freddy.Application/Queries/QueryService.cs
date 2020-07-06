using Freddy.Application.Models;
using System.Linq;

namespace Freddy.Application.Queries
{
    public class QueryService : IQueryService
    {
        IProductViews _products;

        public QueryService(IProductViews products)
        {
            _products = products;
        }

        public ProductView GetProduct(int Id)
        {
            return _products.Products.FirstOrDefault(p => p.Id == Id);
        }

        public ProductView[] GetProducts()
        {
            return _products.Products.ToArray();
        }
    }
}
