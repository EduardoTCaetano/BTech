using BlitzTech.Domain.Entities;

namespace BlitzTech.Domain.Dtos.Category
{
    public class CreateCategoryRequestDto : EntityBase
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
