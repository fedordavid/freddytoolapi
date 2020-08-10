using System;
using System.ComponentModel.DataAnnotations;

namespace Freddy.Persistence.Products
{
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Code { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
    }
}
