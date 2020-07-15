using Freddy.Application.Commands.Products;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Freddy.Application.UnitTests.Utilities
{
    public static class Helpers
    {
        public static Expression<Func<Product, bool>> EqualTo(Guid id, ProductInfo info)
        {
            return p => p.Id == id
                && p.Info.Code == info.Code
                && p.Info.Name == info.Name
                && p.Info.Size == info.Size;
        }
    }
}
