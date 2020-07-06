using Freddy.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Queries
{
    public interface IQueryService
    {
        ProductView GetProduct(int Id);
        ProductView[] GetProducts();
    }
}
