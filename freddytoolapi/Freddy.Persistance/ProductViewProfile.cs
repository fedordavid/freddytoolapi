using AutoMapper;
using Freddy.Application;
using Freddy.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Persistance
{
    public class ProductViewProfile : Profile
    {
        public ProductViewProfile()
        {
            CreateMap<Product, ProductView>();
        }
    }
}
