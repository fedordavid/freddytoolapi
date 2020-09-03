using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freddy.Persistence.Events
{
    [Table("Events")]
    public class EventDescriptorEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(128)]
        [Required]
        public string Stream { get; set; }
        
        public int StreamVersion { get; set; }
        
        public DateTime Created { get; set; }
        
        [Required]
        public string Type { get; set; }
        
        public string Payload { get; set; }
    }
}