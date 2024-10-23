using BlitzTech.Domain.Entities;
using BlitzTech.Domain.Validations;
using System;

namespace BlitzTech.Model
{
    public sealed class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Product(string name, string description, decimal price, int stock, string image, Guid categoryId)
        {
            ValidateAndSetValues(name, description, price, stock, image, categoryId);
        }

        private void ValidateAndSetValues(string name, string description, decimal price, int stock, string image, Guid categoryId)
        {
            ValidateName(name);
            ValidateDescription(description);
            ValidatePrice(price);
            ValidateStock(stock);
            ValidateImage(image);
            ValidateCategoryId(categoryId);

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            CategoryId = categoryId;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                DomainExceptionValidations.ExceptionHandler(true, "Invalid name. Name is required!");
            if (name.Length > 100)
                DomainExceptionValidations.ExceptionHandler(true, "Too long name.");
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                DomainExceptionValidations.ExceptionHandler(true, "Invalid description. Description is required!");
            if (description.Length > 500)
                DomainExceptionValidations.ExceptionHandler(true, "Too long description.");
        }

        private void ValidatePrice(decimal price)
        {
            DomainExceptionValidations.ExceptionHandler(price <= 0, "Invalid Price. Price must be greater than zero!");
        }

        private void ValidateStock(int stock)
        {
            DomainExceptionValidations.ExceptionHandler(stock < 0, "Invalid Stock. Stock must be greater than or equal to zero!");
        }

        private void ValidateImage(string image)
        {
            DomainExceptionValidations.ExceptionHandler(string.IsNullOrEmpty(image), "Invalid Image. Image is required!");
        }

        private void ValidateCategoryId(Guid categoryId)
        {
            DomainExceptionValidations.ExceptionHandler(categoryId == Guid.Empty, "Invalid CategoryId. CategoryId is required!");
        }
    }
}
