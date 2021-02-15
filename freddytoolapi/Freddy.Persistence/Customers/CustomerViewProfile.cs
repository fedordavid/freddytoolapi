using AutoMapper;
using Freddy.Application.Customers.Queries;

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
