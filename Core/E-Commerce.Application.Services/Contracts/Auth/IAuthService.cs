using E_Commerce.Application.Services.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Contracts.Authentication
{
    public interface IAuthService
    {
        Task<UserResultDto> Login(LoginDto loginDto);
        Task<UserResultDto> Register(RegisterDto registerDto);

      
    }
}
