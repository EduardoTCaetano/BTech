using BTech.Domain.Entities;
using System.Collections.Generic;

namespace BTech.Domain.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        void CreateOrder(Order order);
        void DeleteOrder(int orderId);
        void UpdateOrder(Order order);
        void Save();
    }
}
