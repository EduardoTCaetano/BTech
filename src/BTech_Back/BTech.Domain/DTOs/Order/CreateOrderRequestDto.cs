 public class CreateOrderRequestDto
    {
        public Guid UserId { get; set; } // ID do usuário
        public List<CreateOrderItemDto> OrderItems { get; set; } // Itens do pedido
    }