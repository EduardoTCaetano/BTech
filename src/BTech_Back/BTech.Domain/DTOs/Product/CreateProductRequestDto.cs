using System;
using BlitzTech.Domain.Entities;

namespace BlitzTech.Domain.Dtos.Product
{
    public class CreateProductRequestDto : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CategoryId { get; set; }  
    }
}
