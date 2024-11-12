using System;
using BlitzTech.Domain.Entities;

namespace BlitzTech.Domain.DTOs.OrderDTO
{
    public class OrderItemDTO : EntityBase
    {
        public Guid ProductId { get; set; }
        public string NameProd { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }

        public OrderItemDTO(Guid id, Guid productId, string nameProd, decimal price, int quantity, string image)
        {
            Id = id;
            ProductId = productId;
            NameProd = nameProd;
            Price = price;
            Quantity = quantity;
            Image = image;
        }
    }
}
