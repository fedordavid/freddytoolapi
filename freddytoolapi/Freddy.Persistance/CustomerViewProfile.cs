using AutoMapper;
using Freddy.Application.Queries.Customers;
using Freddy.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freddy.Persistance
{
    public class CustomerViewProfile : Profile
    {
        public CustomerViewProfile()
        {
            CreateMap<Customer, CustomerView>();
        }
    }
}
