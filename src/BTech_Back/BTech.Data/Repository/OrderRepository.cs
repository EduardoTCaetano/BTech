using BTech.Domain.Entities;
using BlitzTech.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrderRepository : IOrderRepository
{
    public Task CreateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid userId, Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetByIdAsync(Guid userId, Guid orderId)
    {
        throw new NotImplementedException();
    }
}
