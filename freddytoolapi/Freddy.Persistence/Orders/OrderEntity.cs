using System;
using System.Collections;
using Freddy.Persistence.OrderItems;

namespace Freddy.Persistence.Orders
{
    public class OrderEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
        public OrderStatus Status { get; set; }

        public IEnumerable OrderItems { get; set; }
        
        [Flags]
        public enum OrderStatus
        {
            Completed = 0b_0000_0001,
            Pending = 0b_0000_0010,
            Unresolvable = 0b_0000_0100
        }
    }
}