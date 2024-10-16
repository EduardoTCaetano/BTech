using BTech.Domain.Entities;


namespace BlitzTech.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(Guid userId);
        Task<Order> GetByIdAsync(Guid userId, Guid orderId);
        Task CreateAsync(Order order);
        Task<bool> DeleteAsync(Guid userId, Guid orderId);
    }
}
