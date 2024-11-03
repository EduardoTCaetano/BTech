public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalValue { get; set; }
    public List<OrderItem> OrderItems { get; set; }

    public Order(Guid userId, decimal totalValue, List<OrderItem> orderItems)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TotalValue = totalValue;
        OrderItems = orderItems ?? new List<OrderItem>();
    }

    public Order()
    {
        OrderItems = new List<OrderItem>();
    }
}
