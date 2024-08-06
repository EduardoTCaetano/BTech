using BlitzTech.Domain.Dtos.Category;
using BlitzTech.Domain.Dtos.Product;
using BlitzTech.Domain.DTOs.Product;
using BlitzTech.Model;
using System;

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

        // Mapeamento para Product
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.Image,
                product.IsActive,
                product.CategoryId
            );
        }

        public static Product ToProductFromCreateDTO(this CreateProductRequestDto productDto)
        {
            return new Product
            (
                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.Stock,
                productDto.Image,
                productDto.CategoryId 
            )
            {
                IsActive = productDto.IsActive
            };
        }
    }
}
