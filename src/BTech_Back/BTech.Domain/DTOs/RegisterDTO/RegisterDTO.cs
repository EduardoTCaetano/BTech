using System.ComponentModel.DataAnnotations;

namespace BlitzTech.Domain.DTOs.RegisterDTO
{
    public class RegisterDTO
    {
        [Required]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "O nome de usuário pode conter apenas letras e espaços.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        public string PassWord { get; set; }
    }
}