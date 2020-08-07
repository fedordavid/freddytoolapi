using AutoMapper;
using Freddy.Application.Queries.Products;
using JetBrains.Annotations;

namespace Freddy.Persistence.Products
{
    [UsedImplicitly]
    public class ProductViewProfile : Profile
    {
        public ProductViewProfile()
        {
            CreateMap<ProductEntity, ProductView>();
        }
    }
}
