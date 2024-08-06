using BlitzTech.Domain.Entities;
using System;

namespace BlitzTech.Domain.DTOs.Product
{
    public class ProductDto : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CategoryId { get; set; }

        public ProductDto(Guid id, string name, string description, decimal price, int stock, string image, bool isActive, Guid categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            IsActive = isActive;
            CategoryId = categoryId;
        }
    }
}
