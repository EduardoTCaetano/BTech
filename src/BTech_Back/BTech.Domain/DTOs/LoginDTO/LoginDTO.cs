using System.ComponentModel.DataAnnotations;

namespace BlitzTech.Domain.DTOs.LoginDTO
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}