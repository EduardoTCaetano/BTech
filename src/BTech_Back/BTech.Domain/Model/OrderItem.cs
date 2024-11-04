using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Domain.Entities;

namespace BlitzTech.Model
{
    public class OrderItem : EntityBase
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } 
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}