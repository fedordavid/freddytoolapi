using System;
using Freddy.Application.Customers.Queries;

namespace Freddy.Application.UnitTests.API.Controllers.CustomerController
{
    public class CustomerViewBuilder
    {
        private Guid _id = new Guid("AFB66C37-C61E-468F-8D76-9A1022593666");
        private string _name = "Customer Name";
        private string _email = "customer@name.com";
        private string _phone = "+421900000000";

        public CustomerViewBuilder(CustomerViewBuilder customer)
        {
            _id = customer._id;
            this._name = customer._name;
            this._email = customer._email;
            this._phone = customer._phone;
        }

        public CustomerViewBuilder()
        {
        }

        public CustomerViewBuilder WithId(string id)
        {
            return  new CustomerViewBuilder(this)
            {
                _id = new Guid(id)
            };
        }

        public CustomerView Build()
        {
            return new CustomerView()
            {
                Id = _id,
                Name = _name,
                Email = _email,
                Phone = _phone
            };
        }

        public static implicit operator CustomerView(CustomerViewBuilder builder) => builder.Build();
    }
}