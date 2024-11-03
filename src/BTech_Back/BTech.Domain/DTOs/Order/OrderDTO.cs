using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Domain.Entities;

namespace BlitzTech.Domain.DTOs.OrderDTO
{
    public class OrderDTO : EntityBase
    {
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

        public OrderDTO(Guid id,Guid userId, DateTime orderDate, decimal totalAmount, string status, List<OrderItemDTO> orderItems)
        {
            Id = id;
            UserId = userId;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            Status = status;
            OrderItems = orderItems;
        }
    }
}