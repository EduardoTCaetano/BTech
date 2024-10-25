using BlitzTech.Domain.Dtos.Category;
using BlitzTech.Domain.Dtos.Product;
using BlitzTech.Domain.DTOs.Product;
using BlitzTech.Model;
using BTech.Domain.DTOs.Cart;
using BlitzTech.Domain.Dtos.Order;

namespace BlitzTech.Data.Mapping
{
    public static class AutoMapperProfiles
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto(
                category.Id,
                category.Description,
                category.IsActive
            );
        }

        public static Category ToCategoryFromCreateDTO(this CreateCategoryRequestDto categoryDto)
        {
            return new Category
            (
                categoryDto.Id,
                categoryDto.Description,
                categoryDto.IsActive
            );
        }

        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.Image,
                product.IsActive,
                product.CategoryId
            );
        }

        public static Product ToProductFromCreateDTO(this CreateProductRequestDto productDto)
        {
            return new Product
            (
                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.Stock,
                productDto.Image,
                productDto.CategoryId 
            )
            {
                IsActive = productDto.IsActive
            };
        }

        public static CartItemDto ToCartItemDto(this CartItem cartItem)
        {
            return new CartItemDto(
                cartItem.Id,
                cartItem.ProductId,
                cartItem.UserId,
                cartItem.NameProd,
                cartItem.Image,
                cartItem.Description,
                cartItem.Price,
                cartItem.Quantity
            );
        }

        public static CartItem ToCartItemFromCreateDTO(this CreateCartItemRequestDto cartItemDto)
        {
            return new CartItem
            (
                cartItemDto.ProductId,
                cartItemDto.UserId,
                cartItemDto.NameProd,
                cartItemDto.Image,
                cartItemDto.Description,
                cartItemDto.Price,
                cartItemDto.Quantity
            );
        }

        public static CartItem ToCartItemFromUpdateDTO(this UpdateCartItemRequestDto cartItemDto, CartItem existingCartItem)
        {
            if (existingCartItem == null) throw new ArgumentNullException(nameof(existingCartItem));

            existingCartItem.ProductId = cartItemDto.ProductId;
            existingCartItem.UserId = cartItemDto.UserId;
            existingCartItem.NameProd = cartItemDto.NameProd;
            existingCartItem.Image = cartItemDto.Image;
            existingCartItem.Description = cartItemDto.Description;
            existingCartItem.Price = cartItemDto.Price;
            existingCartItem.Quantity = cartItemDto.Quantity;

            return existingCartItem;
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
            {
                TotalValue = order.TotalValue,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }

        public static Order ToOrderFromDto(this OrderDto orderDto)
        {
            var order = new Order(orderDto.UserId, orderDto.TotalValue);
            order.OrderItems = orderDto.OrderItems.Select(oi => new OrderItem(oi.ProductId, oi.Quantity, oi.UnitPrice)).ToList();
            return order;
        }
    }
}