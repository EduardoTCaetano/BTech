using System.Collections;
using BlitzTech.Model;

namespace BlitzTech.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(Guid id);
        Task<Category> Add(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(Guid id);
        Task<Category> InactiveAsync(Guid id);
    }
}