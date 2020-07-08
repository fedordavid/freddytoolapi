using System;
using System.Threading.Tasks;

namespace Freddy.Application.Commands.Products
{
    public interface IProducts
    {
        Task Add(Product product);
        Task Delete(Guid productId);
    }
}