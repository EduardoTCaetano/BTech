 public class OrderItemDto
    {
        public Guid ProductId { get; set; } // ID do produto
        public string Name { get; set; } // Nome do produto
        public int Quantity { get; set; } // Quantidade do produto
        public decimal UnitPrice { get; set; } // Preço unitário do produto
    }