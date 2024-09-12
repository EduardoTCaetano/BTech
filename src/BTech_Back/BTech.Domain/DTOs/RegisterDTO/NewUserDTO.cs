using System.ComponentModel.DataAnnotations;

namespace BlitzTech.Domain.DTOs.RegisterDTO
{
    public class NewUserDTO
    {
        [Required]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "O nome de usuário pode conter apenas letras e espaços.")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Token { get; set; }

    }
}