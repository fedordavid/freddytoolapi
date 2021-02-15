using System.Threading.Tasks;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Products.Commands.DeleteProduct
{
    public class DeleteProduct : IHandleCommands<DeleteProductCommand>
    {
        private readonly IProducts _products;

        public DeleteProduct(IProducts products)
        {
            _products = products;
        }

        public Task Handle(DeleteProductCommand command)
        {
            return _products.Delete(command.ProductId);
        }
    }
}
