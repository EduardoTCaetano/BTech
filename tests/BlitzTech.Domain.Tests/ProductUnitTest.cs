// using BlitzTech.Domain.Validations;
// using BlitzTech.Model;
// using FluentAssertions;

// namespace BlitzTech.Domain.Tests
// {
//     public class ProductUnitTest
//     {
//         [Fact]
//         public void WhenProductNameLengthExceedsLimit_DomainException()
//         {
//             Action product=()=> new Product("","s", 699, 1, "bbdfgdfgfdgdgd" );
//             product.Should().Throw<DomainExceptionValidations>()
//             .WithMessage("Invalid product name. Name exceeds maximum length of 30 characters.");
//         }
//     }
// }