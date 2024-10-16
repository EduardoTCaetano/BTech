
using BlitzTech.Data.Mapping;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization; // Autorização
using Microsoft.AspNetCore.Mvc; // Controlador MVC
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Suporte assíncrono

namespace BlitzTech.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo; // Interface do repositório de pedidos

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo; // Injeta o repositório de pedidos
        }

        // Método para obter todos os pedidos de um usuário
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid userId)
        {
            var orders = await _orderRepo.GetAllAsync(userId); // Recupera os pedidos do usuário
            if (orders == null || !orders.Any())
            {
                return NotFound("Nenhum pedido encontrado para este usuário."); // Retorna erro se não houver pedidos
            }

            var orderDtos = orders.Select(o => o.ToOrderDto()); // Converte os pedidos em DTOs
            return Ok(orderDtos); // Retorna os pedidos em DTO
        }

        // Método para obter um pedido específico pelo ID
        [HttpGet("{userId}/order/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            var order = await _orderRepo.GetByIdAsync(userId, id); // Recupera o pedido pelo ID e ID do usuário

            if (order == null)
            {
                return NotFound("Pedido não encontrado."); // Retorna erro se o pedido não for encontrado
            }

            return Ok(order.ToOrderDto()); // Retorna o pedido em DTO
        }

        // Método para criar um novo pedido
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderRequestDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Retorna erro se o modelo não for válido

            var order = orderDto.ToOrder(); // Converte o DTO em um objeto de pedido
            await _orderRepo.CreateAsync(order); // Cria o pedido no repositório
            return CreatedAtAction(nameof(GetById), new { userId = order.UserId, id = order.Id }, order.ToOrderDto()); // Retorna o pedido criado
        }

        // Método para excluir um pedido
        [Authorize(Roles = "User,Admin")]
        [HttpDelete("{userId}/order/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            bool isDeleted = await _orderRepo.DeleteAsync(userId, id); // Tenta excluir o pedido

            if (!isDeleted)
            {
                return NotFound("Pedido não encontrado."); // Retorna erro se o pedido não for encontrado
            }

            return NoContent(); // Retorna NoContent se a exclusão foi bem-sucedida
        }
    }
}
