using BlitzTech.Data.Context;
using BlitzTech.Domain.Dtos.Category;
using BlitzTech.Domain.Helpers;
using BlitzTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlitzTech.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Model.Category> CreateAsync(Model.Category categoryModel)
        {
            await _context.Category.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Model.Category?> DeleteAsync(Guid Id)
        {
            var categoryModel = await _context.Category.FirstOrDefaultAsync(x => x.Id == Id);

            if (categoryModel == null)
            {
                return null;
            }

            _context.Category.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<List<Model.Category>> GetAllAsync(QueryObject query)
        {
            var Category =  _context.Category.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Description))
            {
                Category = Category.Where(s => s.Description.Contains(query.Description));
            }
            return await Category.ToListAsync();
        }

        public async Task<Model.Category?> GetByIdAsync(Guid Id)
        {
            return await _context.Category.FindAsync(Id);
        }

        public async Task<Model.Category?> UpdateAsync(Guid Id, UpdateCategoryRequestDto categoryDto)
        {
            var exitingCategory = await _context.Category.FirstOrDefaultAsync(x => x.Id == Id);

            if (exitingCategory == null)
            {
                return null;
            }

            exitingCategory.Description = categoryDto.Description;
            exitingCategory.IsActive = categoryDto.IsActive;

            await _context.SaveChangesAsync();
            return exitingCategory;
        }
    }
}