//using BlitzTech.Domain.Entities;
//using BlitzTech.Domain.Validations;
//using BlitzTech.Model;
//using FluentAssertions;
//using System;
//using Xunit;

//namespace BlitzTech.Domain.Tests.Entities
//{
//    public class ProductUnitTest
//    {
//        [Fact]
//        public void CreateProduct_WithValidParameters_ShouldCreateProduct()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            var product = new Product("Product Name", "Product Description", 100.00m, 10, "image.png", category);

//            // Assert
//            product.Name.Should().Be("Product Name");
//            product.Description.Should().Be("Product Description");
//            product.Price.Should().Be(100.00m);
//            product.Stock.Should().Be(10);
//            product.Image.Should().Be("image.png");
//            product.CategoryId.Should().Be(category);
//        }

//        [Fact]
//        public void CreateProduct_WithEmptyName_ShouldThrowException()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            Action act = () => new Product("", "Product Description", 100.00m, 10, "image.png", category);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Invalid name. Name is required!");
//        }

//        [Fact]
//        public void CreateProduct_WithLongName_ShouldThrowException()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            Action act = () => new Product(new string('a', 31), "Product Description", 100.00m, 10, "image.png", category);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Too long name.");
//        }

//        [Fact]
//        public void CreateProduct_WithEmptyDescription_ShouldThrowException()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            Action act = () => new Product("Product Name", "", 100.00m, 10, "image.png", category);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Invalid description. Description is required!");
//        }

//        [Fact]
//        public void CreateProduct_WithLongDescription_ShouldThrowException()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            Action act = () => new Product("Product Name", new string('a', 31), 100.00m, 10, "image.png", category);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Too long description.");
//        }

//        [Fact]
//        public void CreateProduct_WithNegativePrice_ShouldThrowException()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            Action act = () => new Product("Product Name", "Product Description", -10.00m, 10, "image.png", category);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Invalid Price. Price must be greater than zero!");
//        }

//        [Fact]
//        public void CreateProduct_WithZeroPrice_ShouldThrowException()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            Action act = () => new Product("Product Name", "Product Description", 0.00m, 10, "image.png", category);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Invalid Price. Price must be greater than zero!");
//        }

//        [Fact]
//        public void CreateProduct_WithNegativeStock_ShouldThrowException()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            Action act = () => new Product("Product Name", "Product Description", 100.00m, -10, "image.png", category);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Invalid Stock. Stock must be greater than or equal to zero!");
//        }

//        [Fact]
//        public void CreateProduct_WithEmptyImage_ShouldThrowException()
//        {
//            // Arrange
//            var category = new Category { Id = Guid.NewGuid(), Name = "Electronics" };

//            // Act
//            Action act = () => new Product("Product Name", "Product Description", 100.00m, 10, "", category);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Invalid Image. Image is required!");
//        }

//        [Fact]
//        public void CreateProduct_WithNullCategory_ShouldThrowException()
//        {
//            // Act
//            Action act = () => new Product("Product Name", "Product Description", 100.00m, 10, "image.png", null);

//            // Assert
//            act.Should().Throw<DomainException>().WithMessage("Invalid Category. Category is required!");
//        }
//    }
//}
