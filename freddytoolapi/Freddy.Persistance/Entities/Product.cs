using System;
using System.ComponentModel.DataAnnotations;

namespace Freddy.Persistance.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Code { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
    }
}
