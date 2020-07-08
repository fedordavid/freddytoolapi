using AutoMapper;
using Freddy.Application.Queries.Products;
using Freddy.Persistance.Entities;
using JetBrains.Annotations;

namespace Freddy.Persistance
{
    [UsedImplicitly]
    public class ProductViewProfile : Profile
    {
        public ProductViewProfile()
        {
            CreateMap<Product, ProductView>();
        }
    }
}
