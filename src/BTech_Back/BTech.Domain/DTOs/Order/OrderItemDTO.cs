using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Domain.Entities;

namespace BlitzTech.Domain.DTOs.OrderDTO
{
    public class OrderItemDTO : EntityBase
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public OrderItemDTO(Guid id,Guid productId, string productName, decimal unitPrice, int quantity)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}