using BlitzTech.Domain.Validations;
using BlitzTech.Model;
using FluentAssertions;

namespace BlitzTech.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Fact]
         public void WhenCategoryDescription_LenghtMoreThan_DomainException()
        {
            Action category=()=>  new Category("", true);
            category.Should().Throw<DomainExceptionValidations>()
            .WithMessage("Invalid description. Description is required!");
        }
    }
}