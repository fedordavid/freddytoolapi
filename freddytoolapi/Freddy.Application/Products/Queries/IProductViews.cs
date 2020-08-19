using System.Linq;

namespace Freddy.Application.Products.Queries
{
    public interface IProductViews
    {
        public IQueryable<ProductView> Products { get; }
    }
}
