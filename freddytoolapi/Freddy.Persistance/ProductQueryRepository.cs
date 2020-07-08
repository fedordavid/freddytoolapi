using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freddy.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Freddy.Application.Queries.Products;

namespace Freddy.Persistance
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
