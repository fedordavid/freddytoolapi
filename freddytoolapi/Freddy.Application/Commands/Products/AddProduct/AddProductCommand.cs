using System;
using Freddy.Application.Core.Commands;

namespace Freddy.Application.Commands.Products
{
    public class AddProductCommand : Command
    {
        public Guid Id { get; }
        
        public ProductInfo Info { get; }

        public AddProductCommand(Guid id, ProductInfo info)
        {
            Id = id;
            Info = info;
        }
    }
}