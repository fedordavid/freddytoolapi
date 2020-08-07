using System;

namespace Freddy.Application.Queries.Customers
{
    public class CustomerView
    {
        public Guid Id { get; set; }

        public string Name{ get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
