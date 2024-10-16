using System;
using System.Collections.Generic;

namespace BTech.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } // ID do pedido
        public Guid UserId { get; set; } // ID do usuário que fez o pedido
        public List<OrderItem> OrderItems { get; set; } // Itens do pedido
        public decimal TotalAmount { get; set; } // Valor total do pedido
        public DateTime CreatedAt { get; set; } // Data de criação do pedido
        public string Status { get; set; } // Status do pedido (por exemplo, "PENDING", "COMPLETED")

        public Order()
        {
            OrderItems = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow; // Define a data de criação como a hora atual
            Status = "PENDING"; // O pedido é criado como pendente
        }
    }

   
}
