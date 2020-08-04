using System;

namespace Freddy.Application.Commands.Customers
{
    public class Customer
    {
        public Guid Id { get; set; }
        public CustomerInfo Info { get; set; }

        public Customer(Guid id, CustomerInfo info)
        {
            Id = id;
            Info = info;
        }

        public Customer() { }
    }
}
