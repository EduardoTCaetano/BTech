using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Domain.Entities;
using BlitzTech.Model;

namespace BlitzTech.Domain.DTOs.OrderDTO
{
    public class CreateOrderItemDTO : EntityBase
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public CreateOrderItemDTO(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }


        public OrderItem ToOrderItem()
        {
            return new OrderItem
            {
                ProductId = ProductId,
                ProductName = ProductName,
                UnitPrice = UnitPrice,
                Quantity = Quantity
            };
        }
    }
}