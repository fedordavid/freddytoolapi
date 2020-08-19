using System;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Products.Commands.UpdateProduct
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

