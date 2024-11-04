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
        public decimal TotalAmount { get; set; }

        public CreateOrderRequestDTO(Guid userId, List<CreateOrderItemDTO> orderItems, decimal totalAmount)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.");
            if (orderItems == null || !orderItems.Any()) throw new ArgumentException("Order items cannot be empty.");
            if (totalAmount <= 0) throw new ArgumentException("Total amount must be greater than zero.");

            UserId = userId;
            OrderItems = orderItems;
            TotalAmount = totalAmount;
        }

        public Order ToOrder()
        {
            return new Order
            {
                UserId = UserId,
                OrderDate = DateTime.Now,
                TotalAmount = TotalAmount,
                Status = "Pending",
                OrderItems = OrderItems.Select(oi => oi.ToOrderItem()).ToList()
            };
        }
    }
}