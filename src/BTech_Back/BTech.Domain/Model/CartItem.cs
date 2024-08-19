using BlitzTech.Domain.Entities;

public class CartItem : EntityBase
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }  
    public string NameProd { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public CartItem(Guid productId, Guid userId, string nameProd, string image, string description, decimal price, int quantity)
    {
        ProductId = productId;
        UserId = userId;
        NameProd = nameProd;
        Image = image;
        Description = description;
        Price = price;
        Quantity = quantity;
    }
}
