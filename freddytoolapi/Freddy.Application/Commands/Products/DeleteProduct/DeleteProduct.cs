using Freddy.Application.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Commands.Products
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
