using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Data.Mapping;
using BlitzTech.Domain.DTOs.OrderDTO;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BTech.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDTO orderDto)
        {
            try
            {
                var order = orderDto.ToOrder();

                if (order.TotalAmount <= 0)
                {
                    return BadRequest("O valor total do pedido deve ser maior que zero.");
                }

                await _orderRepository.CreateAsync(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order.ToOrderDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar pedido: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(order.ToOrderDto());
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(Guid userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return Ok(orders.Select(o => o.ToOrderDto()));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = orders.Select(order => order.ToOrderDto()).ToList();
            return Ok(orderDtos);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] string status)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();

            await _orderRepository.UpdateStatusAsync(id, status);
            return NoContent();
        }

        [HttpPatch("{orderId}/items/{itemId}")]
        public async Task<IActionResult> UpdateOrderItem(Guid orderId, Guid itemId, [FromBody] CreateOrderItemDTO orderItemDto)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return NotFound();

            var orderItem = await _orderRepository.GetOrderItemByIdAsync(itemId);
            if (orderItem == null) return NotFound();

            orderItem.ProductId = orderItemDto.ProductId;
            orderItem.Image = orderItemDto.Image;
            orderItem.NameProd = orderItemDto.NameProd;
            orderItem.Price = orderItemDto.Price;
            orderItem.Quantity = orderItemDto.Quantity; 

            await _orderRepository.UpdateOrderItemAsync(orderItem);
            return NoContent();
        }

        [HttpDelete("{orderId}/items/{itemId}")]
        public async Task<IActionResult> DeleteOrderItem(Guid orderId, Guid itemId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return NotFound();

            var orderItem = await _orderRepository.GetOrderItemByIdAsync(itemId);
            if (orderItem == null) return NotFound();

            await _orderRepository.DeleteOrderItemAsync(itemId);
            return NoContent();
        }

        [HttpGet("{id}/total")]
        public async Task<IActionResult> CalculateOrderTotal(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();

            var total = order.OrderItems.Sum(item => item.Price * item.Quantity);
            return Ok(new { Total = total });
        }
    }
}
