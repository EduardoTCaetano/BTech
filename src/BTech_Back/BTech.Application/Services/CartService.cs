using System.Collections.Generic;
using BTech.Domain.Entities;

namespace BTech.Application.Services
{
    public interface ICartService
    {
        List<CartItem> GetCartItemsByUserId(int userId);
        void ClearCart(int userId);
    }

    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }

        public List<CartItem> GetCartItemsByUserId(int userId)
        {
            return _context.CartItems.Where(ci => ci.UserId == userId).ToList();
        }

        public void ClearCart(int userId)
        {
            var cartItems = _context.CartItems.Where(ci => ci.UserId == userId).ToList();
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
    }
}
