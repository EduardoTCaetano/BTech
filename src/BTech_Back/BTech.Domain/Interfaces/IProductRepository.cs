using BlitzTech.Domain.Dtos.Product;
using BlitzTech.Domain.DTOs.Product;
using BlitzTech.Domain.Helpers;
using BlitzTech.Model;


namespace BlitzTech.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);
        Task<Product> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(Product productModel);
        Task<Product> UpdateAsync(Guid id, UpdateProductRequestDto updateDto);
        Task<Product> DeleteAsync(Guid id);
    }
}
