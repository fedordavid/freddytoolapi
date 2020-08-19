using AutoMapper;
using Freddy.Application.Products.Queries;
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
