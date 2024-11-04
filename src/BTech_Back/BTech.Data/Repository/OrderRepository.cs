using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Data.Context;
using BlitzTech.Domain.Interfaces;
using BlitzTech.Model;
using Microsoft.EntityFrameworkCore;

namespace BlitzTech.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the order.", ex);
            }
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id)
                ?? throw new KeyNotFoundException("Order not found.");
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(Guid id, string status)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(Guid id)
        {
            return await _context.OrderItems.FindAsync(id)
                ?? throw new KeyNotFoundException("Order item not found.");
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem == null) throw new ArgumentNullException(nameof(orderItem));

            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(Guid id)
        {
            var orderItem = await GetOrderItemByIdAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}