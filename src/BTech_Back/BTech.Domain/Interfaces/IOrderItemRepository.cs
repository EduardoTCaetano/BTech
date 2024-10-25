using System.Collections.Generic;
using System.Threading.Tasks;
using BlitzTech.Model;

namespace BlitzTech.Data.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId);
        Task<OrderItem> GetOrderItemByIdAsync(Guid id);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(Guid id);
    }
}