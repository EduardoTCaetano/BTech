using BlitzTech.Data.Context;
using BlitzTech.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlitzTech.Data.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DataContext _context;

        public OrderItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(Guid id)
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(Guid id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
