using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freddy.Application.Products.Queries;
using Freddy.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Freddy.Persistence.Products
{
    public class ProductQueryRepository : IProductViews
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ProductQueryRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IQueryable<ProductView> Products => _context.Products.AsNoTracking().ProjectTo<ProductView>(_mapper.ConfigurationProvider);
    }
}
