using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlitzTech.Domain.Dtos.Order
{
    public class CreateOrderRequestDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
