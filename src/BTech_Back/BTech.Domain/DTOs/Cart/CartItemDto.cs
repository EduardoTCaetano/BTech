using System.ComponentModel.DataAnnotations;
using BlitzTech.Domain.Entities;

namespace BTech.Domain.DTOs.Cart
{
    public class CartItemDto : EntityBase
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string NameProd { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; } = 1; 
        public CartItemDto(Guid id, Guid productId, Guid userId, string nameProd, string image, string description, decimal price, int quantity)
        {
            Id = id;
            ProductId = productId;
            UserId = userId;
            NameProd = nameProd;
            Image = image;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }
}