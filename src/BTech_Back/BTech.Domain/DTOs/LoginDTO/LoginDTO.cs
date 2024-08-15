using System.ComponentModel.DataAnnotations;

namespace BlitzTech.Domain.DTOs.LoginDTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress] 
        public string EmailAddress {get; set;}
        [Required]
        public string Password { get; set; }
    }
}