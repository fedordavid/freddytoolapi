using System;

namespace Freddy.Application.Customers.Commands
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

        public Customer With(CustomerInfo info)
        {
            return new Customer(Id, info);
        }
        
        // public IEnumerable<CustomerUpdated> UpdateInfo(CustomerInfo info)
        // {
        //     yield return new CustomerUpdated(Id, info);
        // }
        //
        // public Customer Apply(CustomerUpdated updated)
        // {
        //     return new Customer(Id, updated.Info);
        // }
        // public class CustomerUpdated
        // {
        //     public Guid Id { get; }
        //     public CustomerInfo Info { get; }
        //
        //     public CustomerUpdated(Guid id, CustomerInfo info)
        //     {
        //         Id = id;
        //         Info = info;
        //     }
        // }
    }
}
