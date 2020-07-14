using Freddy.Application.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Commands.Products.UpdateProduct
{
    public class UpdateProduct : IHandleCommands<UpdateProductCommand>
    {
        private readonly IProducts _products;

        public UpdateProduct(IProducts products)
        {
            _products = products;
        }

        public Task Handle(UpdateProductCommand command)
        {
            return  _products.Update(new Product(command.Id, command.ProductInfo));
        }
    }
}
