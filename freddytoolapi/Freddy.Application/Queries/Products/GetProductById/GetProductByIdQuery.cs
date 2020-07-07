using System;
using Freddy.Application.Core.Queries;

namespace Freddy.Application.Queries.Products
{
    public class GetProductByIdQuery : Query<ProductView>
    {
        public Guid ProductId { get; }

        public GetProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }
}
