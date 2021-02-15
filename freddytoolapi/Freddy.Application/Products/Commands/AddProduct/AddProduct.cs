using System.Threading.Tasks;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Products.Commands.AddProduct
{
    public class AddProduct : IHandleCommands<AddProductCommand>
    {
        private readonly IProducts _products;

        public AddProduct(IProducts products)
        {
            _products = products;
        }

        public Task Handle(AddProductCommand command)
        {
            return _products.Add(new Product(command.Id, command.Info));
        }
    }
}