using Freddy.Application.Queries.Customers;
using System;

namespace Freddy.Application.UnitTests.API.Controllers.CustomerController
{
    public class CustomerViewBuilder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public CustomerViewBuilder(CustomerViewBuilder customer)
        {
            this.Id = customer.Id;
            this.Name = customer.Name;
            this.Email = customer.Email;
            this.Phone = customer.Phone;
        }

        public CustomerViewBuilder()
        {

        }

        public CustomerViewBuilder WithId(string id)
        {
            var result = new CustomerViewBuilder(this)
            {
                Id = new Guid(id)
            };

            return result;
        }

        public CustomerView Build()
        {
            return new CustomerView()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Phone = Phone
            };
        }

        public static implicit operator CustomerView(CustomerViewBuilder builder) => builder.Build();
    }
}