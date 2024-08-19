using BlitzTech.Data.Context;
using BTech.Domain.DTOs.Cart;
using BTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlitzTech.Data.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext _context;

        public CartItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cartItems = await _context.CartItem
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.CartItem.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem> CreateAsync(CartItem cartItem)
        {
            await _context.CartItem.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid id)
        {
            var cartItem = await _context.CartItem
                .Where(c => c.UserId == userId && c.Id == id)
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                return false;
            }

            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<CartItem>> GetAllAsync(Guid userId)
        {
            return await _context.CartItem
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<CartItem?> GetByIdAsync(Guid id)
        {
            return await _context.CartItem.FindAsync(id);
        }

        public async Task<CartItem?> UpdateAsync(Guid id, UpdateCartItemRequestDto cartItemDto)
        {
            var existingCartItem = await _context.CartItem.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCartItem == null)
            {
                return null;
            }

            existingCartItem.ProductId = cartItemDto.ProductId;
            existingCartItem.UserId = cartItemDto.UserId;
            existingCartItem.NameProd = cartItemDto.NameProd;
            existingCartItem.Image = cartItemDto.Image;
            existingCartItem.Description = cartItemDto.Description;
            existingCartItem.Price = cartItemDto.Price;
            existingCartItem.Quantity = cartItemDto.Quantity;

            await _context.SaveChangesAsync();
            return existingCartItem;
        }
    }
}
