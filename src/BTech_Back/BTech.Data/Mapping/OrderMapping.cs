using BTech.Domain.Entities;

namespace BlitzTech.Data.Mapping
{
    public static class OrderMapping
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList(),
                TotalAmount = order.TotalAmount,
                CreatedAt = order.CreatedAt,
                Status = order.Status
            };
        }

        public static Order ToOrder(this CreateOrderRequestDto orderDto)
        {
            return new Order
            {
                UserId = orderDto.UserId,
                TotalAmount = orderDto.OrderItems.Sum(item => item.Quantity * item.UnitPrice), // Calcula o total
                OrderItems = orderDto.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
        }
    }
}
