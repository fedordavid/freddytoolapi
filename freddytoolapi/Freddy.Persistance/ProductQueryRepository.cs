using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freddy.Application.Models;
using Freddy.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
