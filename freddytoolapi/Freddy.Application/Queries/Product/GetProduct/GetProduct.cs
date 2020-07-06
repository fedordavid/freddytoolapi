using Freddy.Application.Core;
using Freddy.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Queries.Product.GetProduct
{
    public class GetProduct : IExecuteQuery<GetProductQuery, ProductView>
    {
        private IProductViews _products;

        public GetProduct(IProductViews products)
        {
            _products = products;
        }

        //public ProductView Execute(GetProductQuery query) => query.Apply(_product.Products).ToResult();
        public ProductView Execute(GetProductQuery query)
        {
            return _products.Products.FirstOrDefault(p => p.Id == query.ProductId);
        }
    }

    public static class ProductQueryableExtensions
    {
        public static ProductView[] ToResult(this IQueryable<ProductView> products)
        {
            return products.ToArray();
        }
    }
}
