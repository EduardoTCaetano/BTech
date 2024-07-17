using BlitzTech.Data.Context;
using BlitzTech.Data.Migrations;
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
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Model.Category?> DeleteAsync(Guid Id)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);

            if (categoryModel == null)
            {
                return null;
            }

            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<List<Model.Category>> GetAllAsync(QueryObject query)
        {
            var categories =  _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Description))
            {
                categories = categories.Where(s => s.Description.Contains(query.Description));
            }
            return await categories.ToListAsync();
        }

        public async Task<Model.Category?> GetByIdAsync(Guid Id)
        {
            return await _context.Categories.FindAsync(Id);
        }

        public async Task<Model.Category?> UpdateAsync(Guid Id, UpdateCategoryRequestDto categoryDto)
        {
            var exitingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);

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