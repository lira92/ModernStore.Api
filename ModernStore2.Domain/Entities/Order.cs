using FluentValidator;
using ModernStore.Shared.Entities;
using ModernStore2.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernStore2.Domain.Entities
{
    public class Order:Entity
    {
        private readonly IList<OrderItem> _items;

        protected Order()
        {

        }

        public Order(Customer customer, decimal deliveryFee, decimal discount)
        {
            CreateDate = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();
            DeliveryFee = deliveryFee;
            Discount = discount;
            Customer = customer;

            new ValidationContract<Order>(this)
                .IsGreaterThan(x => x.DeliveryFee, 0)
                .IsGreaterThan(x => x.Discount, -1);
        }

        public Customer Customer { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string Number { get; private set; }
        public EOrderStatus Status { get; private set; }
        public ICollection<OrderItem> Items { get { return _items.ToArray(); } }
        public decimal DeliveryFee { get; private set; }
        public decimal Discount { get; private set; }

        public decimal SubTotal() => Items.Sum(x => x.Total());
        public decimal Total() => SubTotal() + DeliveryFee - Discount;

        public void AddItem(OrderItem item)
        {
            AddNotifications(item.Notifications);
            if (item.IsValid())
                _items.Add(item);
        }

        public void Place()
        {
            //Validações

            Status = EOrderStatus.InProgress;
        }
    }
}
