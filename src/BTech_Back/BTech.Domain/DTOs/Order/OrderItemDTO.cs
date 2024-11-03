using System;
using System.ComponentModel.DataAnnotations;

public class OrderItemDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1.")]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "The unit price must be greater than zero.")]
    public decimal UnitPrice { get; set; }
}