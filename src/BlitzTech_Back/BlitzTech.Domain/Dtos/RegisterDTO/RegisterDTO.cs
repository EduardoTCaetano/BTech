using System.ComponentModel.DataAnnotations;

namespace BlitzTech.Domain.DTOs.RegisterDTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName {get; set;}
        
        [Required]
        [EmailAddress] 
        public string EmailAddress {get; set;}

        [Required]
        public string PassWord {get; set;}
    }
}