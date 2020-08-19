using System;
using System.Threading.Tasks;

namespace Freddy.Application.Products.Commands
{
    public interface IProducts
    {
        Task Add(Product product);
        Task Delete(Guid productId);
        Task Update(Product product);
        Task<Product> Get(Guid productId);
    }
}