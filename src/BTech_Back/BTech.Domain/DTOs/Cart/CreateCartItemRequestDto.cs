using System.ComponentModel.DataAnnotations;
using BlitzTech.Domain.Entities;

namespace BTech.Domain.DTOs.Cart
{
    public class CreateCartItemRequestDto : EntityBase
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
        public int Quantity { get; set; }
    }
}