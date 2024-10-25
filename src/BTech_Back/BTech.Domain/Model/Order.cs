using System;
using System.Collections.Generic;

namespace BlitzTech.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 

        public Order() 
        {
            OrderDate = DateTime.UtcNow;
            Status = "Pending";
        }

        public Order(Guid userId, decimal totalValue) : this()
        {
            Id = Guid.NewGuid();
            UserId = userId;
            TotalValue = totalValue;
        }

        public Order(Guid userId, decimal totalValue, List<OrderItem> orderItems) : this(userId, totalValue)
        {
            OrderItems = orderItems;
        }
    }
}
