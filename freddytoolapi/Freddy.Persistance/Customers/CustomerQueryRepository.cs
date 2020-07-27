using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freddy.Application.Queries;
using Freddy.Application.Queries.Customers;
using Freddy.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistance.Customers
{
    public class CustomerQueryRepository : ICustomerViews
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CustomerQueryRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<CustomerView> Customers => _context.Customers.AsNoTracking().ProjectTo<CustomerView>(_mapper.ConfigurationProvider);
    }
}
