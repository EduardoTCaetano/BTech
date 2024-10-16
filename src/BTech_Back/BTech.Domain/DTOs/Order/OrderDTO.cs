public class OrderDto
    {
        public Guid Id { get; set; } // ID do pedido
        public Guid UserId { get; set; } // ID do usuário
        public List<OrderItemDto> OrderItems { get; set; } // Itens do pedido
        public decimal TotalAmount { get; set; } // Valor total do pedido
        public DateTime CreatedAt { get; set; } // Data de criação
        public string Status { get; set; } // Status do pedido
    }