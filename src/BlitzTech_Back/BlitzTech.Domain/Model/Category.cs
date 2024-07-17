using System.ComponentModel.DataAnnotations;
using BlitzTech.Domain.Entities;
using BlitzTech.Domain.Validations;

namespace BlitzTech.Model
{
    public class Category : EntityBase
    {
        [Required]
        [MinLength(3, ErrorMessage = "Nome muito curto")]
        [MaxLength(15, ErrorMessage = "Nome muito grande")]

        public string Description { get;  set; } 
        public bool IsActive { get; set; }

        public Category(Guid id, string description, bool isActive)
        {
            ValidateDescription(description);

            Id = id;
            Description = description;
            IsActive = isActive;
        }

        public void ValidateDescription(string description)
        {
            DomainExceptionValidations.ExceptionHandler(string.IsNullOrEmpty(description),
                "Invalid Description. Description is required!");
        }

        public void ValidateActive(string isActive)
        {
            DomainExceptionValidations.ExceptionHandler(bool.Parse(isActive),
                "Invalid Active. IsActive is required!");
        }
    }
}
