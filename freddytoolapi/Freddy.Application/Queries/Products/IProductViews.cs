using System.Linq;

namespace Freddy.Application.Queries.Products
{
    public interface IProductViews
    {
        public IQueryable<ProductView> Products { get; }
    }
}
