using BTech.Domain.Entities;
using BTech.Domain.Interfaces;
using BTech.Application.Services;
using BTech.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTech.Application.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;

        public OrdersController(IOrderRepository orderRepository, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
        }

        // GET: api/orders
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> GetOrders()
        {
            var orders = _orderRepository.GetAllOrders();

            var orderDtos = orders.Select(order => new OrderDTO
            {
                UserId = order.UserId,
                Total = order.Total,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(item => new OrderItemDTO
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            }).ToList();

            return Ok(orderDtos);
        }

        // GET: api/orders/5
        [HttpGet("{id:guid}")]
        public ActionResult<OrderDTO> GetOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDto = new OrderDTO
            {
                UserId = order.UserId,
                Total = order.Total,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(item => new OrderItemDTO
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice 
                }).ToList()
            };

            return Ok(orderDto);
        }

        // POST: api/orders/create
        [HttpPost("create")]
        public IActionResult CreateOrder(CreateOrderDTO createOrderDto)
        {
            // Obtém os itens do carrinho do usuário
            var cartItems = _cartService.GetCartItemsByUserId(createOrderDto.UserId);
            if (cartItems == null || cartItems.Count == 0)
            {
                return BadRequest("O carrinho está vazio.");
            }

            // Calcula o total do pedido com base nos itens do carrinho
            var total = cartItems.Sum(item => item.Quantity * item.Price);

            // Cria um novo pedido
            var order = new Order
            {
                UserId = createOrderDto.UserId,
                OrderDate = DateTime.Now,
                Total = total,
                Items = cartItems.Select(cartItem => new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Price 
                }).ToList()
            };

            // Salva o pedido no banco de dados
            _orderRepository.CreateOrder(order);
            _orderRepository.Save();

            // Limpa o carrinho do usuário
            _cartService.ClearCart(createOrderDto.UserId);

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order.OrderId);
        }

        // DELETE: api/orders/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderRepository.DeleteOrder(id);
            _orderRepository.Save();
            return Ok();
        }
    }
}
