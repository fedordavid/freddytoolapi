using System;
using System.ComponentModel.DataAnnotations;

namespace Freddy.Persistance.Customers
{
    public class CustomerEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
