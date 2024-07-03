using System.Collections;
using BlitzTech.Domain.Dtos.Category;
using BlitzTech.Domain.Helpers;
using BlitzTech.Model;

namespace BlitzTech.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(QueryObject query);
        Task<Category?> GetByIdAsync(Guid Id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(Guid Id, UpdateCategoryRequestDto categoryDto);
        Task<Category?> DeleteAsync(Guid Id);
    }
}