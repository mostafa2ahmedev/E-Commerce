using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.DTO.Authentication
{
    public class UserResultDto() {

        public required string Id { get; set; }
        public required string DisplayName { get; set; } = null!;
        public required string Token {  get; set; } = null!;
        public required string Email { get; set; } = null!;


    }
 
}
