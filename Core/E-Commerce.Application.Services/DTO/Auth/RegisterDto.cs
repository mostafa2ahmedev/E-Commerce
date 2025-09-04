using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.DTO.Authentication
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{5,}$")]
        public string Password { get; set; } = null!;
        [Phone]      
        public string? PhoneNumber { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string DisplayName { get; set; } = null!;
    

    }
}
