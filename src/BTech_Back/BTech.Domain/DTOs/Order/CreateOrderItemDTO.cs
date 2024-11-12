using System;
using BlitzTech.Domain.Entities;
using BlitzTech.Model;

namespace BlitzTech.Domain.DTOs.OrderDTO
{
    public class CreateOrderItemDTO : EntityBase
    {
        public Guid ProductId { get; set; }
        public string NameProd { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }

        public CreateOrderItemDTO(Guid productId, string nameProd, decimal price, int quantity, string image)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
            ProductId = productId;
            NameProd = nameProd;
            Price = price;
            Quantity = quantity;
            Image = image;
        }

        public OrderItem ToOrderItem()
        {
            return new OrderItem
            {
                ProductId = ProductId,
                NameProd = NameProd,
                Price = Price,
                Quantity = Quantity,
                Image = Image
            };
        }
    }
}
