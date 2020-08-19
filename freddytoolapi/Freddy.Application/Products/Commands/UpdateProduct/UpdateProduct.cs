using System.Threading.Tasks;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Products.Commands.UpdateProduct
{
    public class UpdateProduct : IHandleCommands<UpdateProductCommand>
    {
        private readonly IProducts _products;

        public UpdateProduct(IProducts products)
        {
            _products = products;
        }

        public async Task Handle(UpdateProductCommand command)
        {
            var product = await _products.Get(command.Id);

            if (product is null)
                throw new NotFoundException();
            
            var updatedProduct = product.With(command.ProductInfo);
            await _products.Update(updatedProduct);
        }
    }
}
