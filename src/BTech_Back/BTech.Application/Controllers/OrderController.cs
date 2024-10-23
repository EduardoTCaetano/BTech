using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Model;
using BlitzTech.Data.Context;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var orderItems = orderDto.OrderItems.Select(i => new OrderItem(i.ProductId, i.Quantity, i.UnitPrice)).ToList();
        var order = new Order(orderDto.UserId, orderDto.TotalValue, orderItems);

        await _orderRepository.AddAsync(order);

        return Ok(order);
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
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDto orderDto)
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

        order.TotalValue = orderDto.TotalValue;
        order.OrderItems = orderDto.OrderItems.Select(i => new OrderItem(i.ProductId, i.Quantity, i.UnitPrice)).ToList();

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