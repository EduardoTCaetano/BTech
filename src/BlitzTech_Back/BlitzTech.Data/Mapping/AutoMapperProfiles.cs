using BlitzTech.Domain.Dtos.Category;
using BlitzTech.Model;

namespace BlitzTech.Data.Mapping
{
    public static class AutoMapperProfiles
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto(
                category.Id,
                category.Description,
                category.IsActive
            );
        }

        public static Category ToCategoryFromCreateDTO(this CreateCategoryRequestDto categoryDto)
        {
            return new Category
            (
                categoryDto.Id,
                categoryDto.Description,
                categoryDto.IsActive
            );
        }

    }
}
