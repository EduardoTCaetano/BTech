using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Domain.Entities;
using BlitzTech.Model;

namespace BlitzTech.Domain.DTOs.OrderDTO
{
   public class CreateOrderRequestDTO : EntityBase
    {
        public Guid UserId { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; } = new List<CreateOrderItemDTO>();

        public CreateOrderRequestDTO(Guid userId, List<CreateOrderItemDTO> orderItems)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.");
            if (orderItems == null || !orderItems.Any()) throw new ArgumentException("Order items cannot be empty.");

            UserId = userId;
            OrderItems = orderItems;
        }

        public Order ToOrder()
        {
            decimal totalAmount = OrderItems.Sum(item => item.Quantity * item.Price); // Calcula o valor total aqui

            return new Order
            {
                UserId = UserId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Pending",
                OrderItems = OrderItems.Select(oi => oi.ToOrderItem()).ToList()
            };
        }
    }
}