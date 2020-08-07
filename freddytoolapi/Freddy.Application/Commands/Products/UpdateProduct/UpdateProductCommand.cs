using Freddy.Application.Core.Commands;
using System;

namespace Freddy.Application.Commands.Products.UpdateProduct
{
    public class UpdateProductCommand : Command
    {
        public Guid Id { get; set; }
        public ProductInfo ProductInfo { get; set; }

        public UpdateProductCommand(Guid productId, ProductInfo productInfo)
        {
            Id = productId;
            ProductInfo = productInfo;
        }
    }
}

