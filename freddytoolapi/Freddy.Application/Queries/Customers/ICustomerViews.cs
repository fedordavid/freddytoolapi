using Freddy.Application.Queries.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Application.Queries
{
    public interface ICustomerViews
    {
        public IQueryable<CustomerView> Customers { get; }
    }
}
