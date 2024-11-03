using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class OrderDto
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "The total value must be greater than zero.")]
    public decimal TotalValue { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public List<OrderItemDto> OrderItems { get; set; }
}