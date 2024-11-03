using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BTech.Data.Configuration
{
    public class CartItemEntityConfiguration
    {
         public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItem");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.ProductId)
            .IsRequired();

        builder.Property(ci => ci.UserId)
            .IsRequired();

        builder.Property(ci => ci.NameProd)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(ci => ci.Image)
            .HasMaxLength(250)
            .IsUnicode(false);

        builder.Property(ci => ci.Description)
            .HasMaxLength(500)
            .IsUnicode(false);

        builder.Property(ci => ci.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.Quantity)
            .IsRequired();

        builder.HasOne<CartItem>() 
            .WithMany() 
            .HasForeignKey(ci => ci.ProductId);
    }
}}