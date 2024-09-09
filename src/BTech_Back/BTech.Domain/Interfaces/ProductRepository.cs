using BlitzTech.Domain.DTOs.Product;
using BlitzTech.Domain.Helpers;
using BlitzTech.Domain.Interfaces;
using BlitzTech.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzTech.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _context;

        public ProductRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(QueryObject query)
        {
            var queryable = _context.Set<Product>().AsQueryable();

            // Apply search term filter if provided
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var searchTerm = query.SearchTerm.ToLower();
                queryable = queryable.Where(p =>
                    p.Name.ToLower().Contains(searchTerm) ||
                    p.Description.ToLower().Contains(searchTerm)
                );
            }

            // Optionally, apply other filters such as category or sorting here

            return await queryable.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Product>().FindAsync(id);
        }

        public async Task<List<Product>> GetByCategoryAsync(Guid categoryId)
        {
            return await _context.Set<Product>()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product> CreateAsync(Product productModel)
        {
            _context.Set<Product>().Add(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<Product?> UpdateAsync(Guid id, UpdateProductRequestDto updateDto)
        {
            var product = await _context.Set<Product>().FindAsync(id);
            if (product == null)
            {
                return null;
            }

            // Update product properties here
            product.Name = updateDto.Name;
            product.Description = updateDto.Description;
            product.Price = updateDto.Price;
            product.CategoryId = updateDto.CategoryId;

            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var product = await _context.Set<Product>().FindAsync(id);
            if (product == null)
            {
                return null;
            }

            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
