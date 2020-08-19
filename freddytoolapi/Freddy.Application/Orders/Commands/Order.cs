﻿using System;
using System.Collections.Generic;
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
        
        public IEnumerable<Event> CreateOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
                throw new Exception("Order Id is not valid.");
            
            yield return new OrderCreated(orderId, _customerId);
        }
    }
    
    public class Order
    {
        private readonly Guid _orderId;
        private readonly Guid _customerId;
        
        private readonly Dictionary<Guid, OrderItem> _orderItems = new Dictionary<Guid, OrderItem>();

        public Order(Guid orderId, Guid customerId)
        {
            _orderId = orderId;
            _customerId = customerId;
        }
        
        public Order(Order order)
        {
            _orderId = order._orderId;
            _customerId = order._customerId;
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
            
            yield return new ProductQtySet(_orderId, productId, qty);
        }
        
        public IEnumerable<Event> RemoveProduct(Guid productId)
        {
            if (!_orderItems.ContainsKey(productId))
                throw new Exception($"Product `{productId}` can't be removed.");
            
            yield return new ProductRemoved(_orderId, productId);
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
                OrderCreated e => new Order(e.CustomerId, e.OrderId),
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
        public Guid OrderId { get; }
        
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
        public Guid CustomerId { get; }

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
        public Guid ProductId { get; }
        public int Qty { get; }

        public ProductQtySet(Guid orderId, Guid productId, int qty) : base(orderId)
        {
            ProductId = productId;
            Qty = qty;
        }
    }
    
    public class ProductRemoved : OrderEvent
    {
        public Guid ProductId { get; }

        public ProductRemoved(Guid orderId, Guid productId) : base(orderId)
        {
            ProductId = productId;
        }
    }
}