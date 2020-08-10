using Freddy.Application.Core.Commands;
using System;

namespace Freddy.Application.Commands.Products
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
