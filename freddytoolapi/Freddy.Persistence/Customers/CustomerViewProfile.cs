using AutoMapper;
using Freddy.Application.Queries.Customers;

namespace Freddy.Persistence.Customers
{
    public class CustomerViewProfile : Profile
    {
        public CustomerViewProfile()
        {
            CreateMap<CustomerEntity, CustomerView>();
        }
    }
}
