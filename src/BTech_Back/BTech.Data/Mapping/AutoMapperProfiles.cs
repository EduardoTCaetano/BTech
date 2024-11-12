using BlitzTech.Domain.Dtos.Category;
using BlitzTech.Domain.Dtos.Product;
using BlitzTech.Domain.DTOs.OrderDTO;
using BlitzTech.Domain.DTOs.Product;
using BlitzTech.Model;
using BTech.Domain.DTOs.Cart;

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

        public static OrderDTO ToOrderDto(this Order order)
        {
            return new OrderDTO
            (
                order.Id,
                order.UserId,
                order.OrderDate,
                order.TotalAmount,
                order.Status,
                order.OrderItems.Select(oi => oi.ToOrderItemDto()).ToList()
            );
        }

        public static OrderItemDTO ToOrderItemDto(this OrderItem orderItem)
        {
            return new OrderItemDTO
            (
                orderItem.Id,
                orderItem.ProductId,
                orderItem.NameProd,
                orderItem.Price,
                orderItem.Quantity,
                orderItem.Image
            );
        }

        public static Order ToOrderFromCreateDTO(this CreateOrderRequestDTO orderRequestDto)
        {
            return new Order
            {
                UserId = orderRequestDto.UserId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                OrderItems = orderRequestDto.OrderItems.Select(oi => oi.ToOrderItem()).ToList()
            };
        }
        
        public static OrderItem ToOrderItem(this CreateOrderItemDTO orderItemDto)
        {
            if (orderItemDto == null) throw new ArgumentNullException(nameof(orderItemDto));
            
            return new OrderItem
            {
                ProductId = orderItemDto.ProductId,
                NameProd = orderItemDto.NameProd,
                Price = orderItemDto.Price,
                Quantity = orderItemDto.Quantity,
                Image = orderItemDto.Image
            };
        }
    }
}