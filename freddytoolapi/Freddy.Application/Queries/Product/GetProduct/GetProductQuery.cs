using Freddy.Application.Core;
using Freddy.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Queries.Product.GetProduct
{
    public class GetProductQuery : Query<ProductView>
    {
        public int ProductId { get; set; }
    }
}
