using BlitzTech.Data.Context;
using BlitzTech.Domain.DTOs.Product;
using BlitzTech.Domain.Helpers;
using BlitzTech.Domain.Interfaces;
using BlitzTech.Model;
using Microsoft.EntityFrameworkCore;

namespace BlitzTech.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Model.Product> CreateAsync(Model.Product productModel)
        {
            await _context.Product.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public Task<Product> CreateAsync(Category productModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Model.Product?> DeleteAsync(Guid id)
        {
            var productModel = await _context.Product.FirstOrDefaultAsync(x => x.Id == id);

            if (productModel == null)
            {
                return null;
            }

            _context.Product.Remove(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<List<Model.Product>> GetAllAsync(QueryObject query)
        {
            var products = _context.Product.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Description))
            {
                products = products.Where(p => p.Description.Contains(query.Description));
            }

            return await products.ToListAsync();
        }

        public async Task<Model.Product?> GetByIdAsync(Guid id)
        {
            return await _context.Product
                .Include(p => p.Category) // Include related category if needed
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Model.Product?> UpdateAsync(Guid id, UpdateProductRequestDto productDto)
        {
            var existingProduct = await _context.Product.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.Stock = productDto.Stock;
            existingProduct.Image = productDto.Image;
            existingProduct.IsActive = productDto.IsActive;
            existingProduct.CategoryId = productDto.CategoryId; 

            await _context.SaveChangesAsync();
            return existingProduct;
        }

        // Implement the correct DeleteAsync method from the interface
        async Task<Product> IProductRepository.DeleteAsync(Guid id)
        {
            var productModel = await _context.Product.FirstOrDefaultAsync(x => x.Id == id);

            if (productModel == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            _context.Product.Remove(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }
    }
}
