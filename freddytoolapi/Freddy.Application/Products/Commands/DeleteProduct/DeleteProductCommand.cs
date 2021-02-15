using System;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : Command
    {
        public Guid ProductId { get; set; }

        public DeleteProductCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}
