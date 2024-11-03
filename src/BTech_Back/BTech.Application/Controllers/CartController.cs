using BTech.Domain.DTOs.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using BTech.Domain.Interfaces;
using BlitzTech.Data.Mapping;

namespace BlitzTech.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepo;

        public CartController(ICartItemRepository cartItemRepo)
        {
            _cartItemRepo = cartItemRepo;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid userId)
        {
            var cartItems = await _cartItemRepo.GetAllAsync(userId);
            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("Nenhum item no carrinho encontrado para este usuário.");
            }

            var cartItemDtos = cartItems.Select(ci => ci.ToCartItemDto());
            return Ok(cartItemDtos);
        }

        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var cartItem = await _cartItemRepo.GetByIdAsync(id);

            if (cartItem == null)
            {
                return NotFound("Item do carrinho não encontrado.");
            }

            return Ok(cartItem.ToCartItemDto());
        }

        [HttpPost("user/{userId}/items")]
        public async Task<IActionResult> Post([FromBody] CreateCartItemRequestDto cartItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartItem = cartItemDto.ToCartItemFromCreateDTO();
            await _cartItemRepo.CreateAsync(cartItem);
            return CreatedAtAction(nameof(GetById), new { id = cartItem.Id }, cartItem.ToCartItemDto());
        }

        [HttpPut("item/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCartItemRequestDto updateCartItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCartItem = await _cartItemRepo.UpdateAsync(id, updateCartItemDto);

            if (updatedCartItem == null)
            {
                return NotFound("Item do carrinho não encontrado.");
            }

            return Ok(updatedCartItem.ToCartItemDto());
        }

        [HttpDelete("user/{userId}/item/{id}")]
        public async Task<IActionResult> Delete(Guid userId, Guid id)
        {
            bool isDeleted = await _cartItemRepo.DeleteAsync(userId, id);

            if (!isDeleted)
            {
                return NotFound("Item do carrinho não encontrado.");
            }

            return NoContent();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> ClearCart([FromRoute] Guid userId)
        {
            var cartItems = await _cartItemRepo.GetAllAsync(userId);

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("Nenhum item no carrinho encontrado para este usuário.");
            }
            await _cartItemRepo.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
