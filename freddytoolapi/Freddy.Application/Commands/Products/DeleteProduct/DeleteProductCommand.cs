using Freddy.Application.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
