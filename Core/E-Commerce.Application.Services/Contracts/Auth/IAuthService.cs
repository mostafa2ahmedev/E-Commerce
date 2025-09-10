
using E_Commerce.Application.Services.DTO.Authentication;
using E_Commerce.Application.Services.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Contracts.Authentication
{
    public interface IAuthService
    {
        Task<UserResultDto> Login(LoginDto loginDto);
        Task<UserResultDto> Register(RegisterDto registerDto);

        Task<UserResultDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal); 
        Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto addressDto);

        Task<bool> EmailExists(string email);
    }
}
