using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Model;
using BlitzTech.Data.Context;
using Microsoft.EntityFrameworkCore;
using BlitzTech.Domain.Dtos.Order;
using BlitzTech.Data.Interfaces;

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
    [Route("CreateOrder")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.OrderItems == null || !request.OrderItems.Any())
        {
            return BadRequest("Order must have at least one item.");
        }

        var totalValue = 0m;
        var orderItems = new List<OrderItem>();

        foreach (var item in request.OrderItems)
        {
            if (item.Quantity <= 0 || item.UnitPrice <= 0)
            {
                return BadRequest("Invalid item details.");
            }

            totalValue += item.Quantity * item.UnitPrice;
            orderItems.Add(new OrderItem(item.ProductId, item.Quantity, item.UnitPrice));
        }

        var order = new Order(request.UserId, totalValue, orderItems);

        await _orderRepository.AddAsync(order);

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    [HttpGet]
    [Route("GetOrder/{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        return Ok(order);
    }

    [HttpPut]
    [Route("UpdateOrder/{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] CreateOrderRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        order.OrderItems.Clear();
        order.TotalValue = 0;

        foreach (var item in request.OrderItems)
        {
            if (item.Quantity <= 0 || item.UnitPrice <= 0)
            {
                return BadRequest("Invalid item details.");
            }

            var orderItem = new OrderItem(item.ProductId, item.Quantity, item.UnitPrice);
            order.OrderItems.Add(orderItem);
            order.TotalValue += item.Quantity * item.UnitPrice;
        }

        await _orderRepository.UpdateAsync(order);

        return Ok(order);
    }

    [HttpDelete]
    [Route("DeleteOrder/{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        await _orderRepository.DeleteAsync(id);

        return Ok("Order deleted successfully.");
    }
}
