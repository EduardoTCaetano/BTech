using BlitzTech.Domain.Entities;

namespace BlitzTech.Domain.Dtos.Category
{
    public class CategoryDto : EntityBase
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public CategoryDto(Guid id, string description, bool isActive)
        {
            Id = id;
            Description = description;
            IsActive = isActive;
        }


    }
}
