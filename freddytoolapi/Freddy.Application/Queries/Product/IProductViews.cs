using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Models
{
    public interface IProductViews
    {
        public IQueryable<ProductView> Products { get; }
    }
}
