using BTech.Domain.DTOs.Cart;

namespace BTech.Domain.Interfaces
{
    public interface ICartItemRepository
    {
        Task ClearCartAsync(Guid userId);
        Task<CartItem> CreateAsync(CartItem cartItem);
        Task<bool> DeleteAsync(Guid userId, Guid itemId);
        Task<List<CartItem>> GetAllAsync(Guid userId);
        Task<CartItem?> GetByIdAsync(Guid id);
        Task<CartItem?> UpdateAsync(Guid id, UpdateCartItemRequestDto cartItemDto);
    }
}
