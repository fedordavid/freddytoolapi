using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Freddy.Application.Core.Events;

// Create (Customer)
// Add Product 
// Remove Product
// Update Note
// Complete
// Unresolvable

namespace Freddy.Application.Orders.Commands
{
    public class OrderCustomer
    {
        private readonly Guid _customerId;

        public OrderCustomer(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new Exception("Customer Id is not valid.");
            
            _customerId = customerId;
        }
        
        public IEnumerable<OrderEvent> CreateOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
                throw new Exception("Order Id is not valid.");
            
            yield return new OrderCreated(orderId, _customerId);
        }
    }
    
    public class Order
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }

        public ReadOnlyDictionary<Guid, OrderItem> OrderItems => new ReadOnlyDictionary<Guid, OrderItem>(_orderItems);

        private readonly Dictionary<Guid, OrderItem> _orderItems = new Dictionary<Guid, OrderItem>();

        public Order(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
        
        public Order(Order order)
        {
            OrderId = order.OrderId;
            CustomerId = order.CustomerId;
            _orderItems = new Dictionary<Guid, OrderItem>(order._orderItems);
        }
        
        public static IEnumerable<Event> Create(Guid orderId, Guid customerId)
        {
            yield return new OrderCreated(orderId, customerId);
        }
        
        public IEnumerable<Event> SetProductQty(Guid productId, int qty)
        {
            if (qty <= 0)
                throw new Exception($"Product qty can't be `{qty}`. ProductId: {productId}");
            
            yield return new ProductQtySet(OrderId, productId, qty);
        }
        
        public IEnumerable<Event> RemoveProduct(Guid productId)
        {
            if (!_orderItems.ContainsKey(productId))
                throw new Exception($"Product `{productId}` can't be removed.");
            
            yield return new ProductRemoved(OrderId, productId);
        }
        
        public Order Apply(ProductQtySet productQtySet)
        {
            var order = new Order(this)
            {
                _orderItems = { [productQtySet.ProductId] = new OrderItem(productQtySet.ProductId, productQtySet.Qty) }
            };

            return order;
        }
        
        public Order Apply(ProductRemoved productRemoved)
        {
            var order = new Order(this);

            order._orderItems.Remove(productRemoved.ProductId);

            return order;
        }
        
        public static Order Initialize(IEnumerable<Event> events)
        {
            return events.Aggregate(null as Order, (order, @event) => @event switch
            {
                OrderCreated e => new Order(e.OrderId, e.CustomerId),
                OrderEvent e => order.Apply(e as dynamic),
                _ => throw new Exception($"Event `{@event.GetType().Name}` is not supported")
            });
        }
    }
    
    public class OrderItem
    {
        public Guid ProductId { get; }

        public int Qty { get; }
        
        public OrderItem(Guid productId, int qty)
        {
            if (qty <= 0) 
                throw new Exception($"Invalid Qty for product [{ProductId}]");
            
            ProductId = productId;
            Qty = qty;
        }
    }

    public abstract class OrderEvent : Event 
    {
        protected OrderEvent()
        {
        }

        public Guid OrderId { get; set;}
        
        protected OrderEvent(Guid orderId)
        {
            OrderId = orderId;
        }
        
        protected bool Equals(OrderEvent other)
        {
            return OrderId.Equals(other.OrderId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((OrderEvent) obj);
        }

        public override int GetHashCode()
        {
            return OrderId.GetHashCode();
        }
    }
    
    public class OrderCreated : OrderEvent
    {
        public OrderCreated()
        {
        }

        public Guid CustomerId { get; set; }

        public OrderCreated(Guid orderId, Guid customerId) : base(orderId)
        {
            CustomerId = customerId;
        }

        protected bool Equals(OrderCreated other)
        {
            return base.Equals(other) && CustomerId.Equals(other.CustomerId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((OrderCreated) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), CustomerId);
        }
    }
    
    public class ProductQtySet : OrderEvent
    {
        public ProductQtySet()
        {
        }

        public Guid ProductId { get; set;}
        public int Qty { get; set; }

        public ProductQtySet(Guid orderId, Guid productId, int qty) : base(orderId)
        {
            ProductId = productId;
            Qty = qty;
        }
    }
    
    public class ProductRemoved : OrderEvent
    {
        public ProductRemoved()
        {
        }

        public Guid ProductId { get; set;}

        public ProductRemoved(Guid orderId, Guid productId) : base(orderId)
        {
            ProductId = productId;
        }
    }
}