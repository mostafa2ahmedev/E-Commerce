using E_Commerce.Application.Services.Contracts.Authentication;
using E_Commerce.Application.Services.DTO.Authentication;
using E_Commerce.Application.Common.Exceptions;

using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using E_Commerce.Application.Services.DTO.Common;
using AutoMapper;
using E_Commerce.Application.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace E_Commerce.Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserResultDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email!);
            //var user = await _userManager.FindUserWithAddress(claimsPrincipal);
            return new UserResultDto()
            {
                Id = user!.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await CreateTokenAsync(user)


            };
        }

        public async Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindUserWithAddress(claimsPrincipal);

            var address = _mapper.Map<AddressDto>(user!.Address);

            return address;

        }
        public async Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal,AddressDto addressDto)
        {
            //   var address = _mapper.Map<Address>(addressDto);
            //   var user = await _userManager.FindUserWithAddress(claimsPrincipal);

            //   user.Address = address;

            //await   _userManager.UpdateAsync(user);

            var updatedAddress = _mapper.Map<Address>(addressDto);
            var user = await _userManager.FindUserWithAddress(claimsPrincipal);


            if(user.Address is not null)
                updatedAddress.Id = user.Address.Id;

            user.Address = updatedAddress;
            var result=  await _userManager.UpdateAsync(user);

            if (!result.Succeeded) throw new 
                    BadRequestException(result.Errors.Select(error=>error.Description).Aggregate((x,y)=>$"{x}, {y}"));
            return addressDto;
        }
        public async Task<bool> EmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email!) is not null;
        }


        public async Task<UserResultDto> Login(LoginDto loginDto)
        {
            //1. Check if there is a user with that email or not 
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                throw new UnAuthorizedException("Invalid Login");

            //2. Check if this password match the user password or not  
            //var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);

            if (result.IsNotAllowed) throw new UnAuthorizedException("Account Not Confirmed Yet!");

            if (result.IsLockedOut) throw new UnAuthorizedException("Account Locked!");

            if (!result.Succeeded)
                throw new UnAuthorizedException("Invalid Login");
          
                return new UserResultDto()
                {
                    Id  = user.Id,
                    DisplayName = user.DisplayName,
                    Email = user.Email!,
                    Token = await CreateTokenAsync(user)


                };
        }



        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {
            //if (EmailExists(registerDto.Email).Result)
            //    throw new BadRequestException("This email is already Used");
            var user = new ApplicationUser() {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName

            };
            var result =  await _userManager.CreateAsync(user,registerDto.Password);

            if (!result.Succeeded) {

                var errors = result.Errors.Select(E => E.Description).ToList();
                throw new ValidationException() { Errors = errors} ;
;            }

            return new UserResultDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokenAsync(user)


            };
        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {

            // Private Claims (User Defined)

            var userClaims = await _userManager.GetClaimsAsync(user);

            var userRoleClaims =new List<Claim>();

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles) {
                userRoleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

         
            var authClaims = new List<Claim>() {
            new Claim(ClaimTypes.PrimarySid,user.Id),
            new Claim(ClaimTypes.Name,user.DisplayName),
            new Claim(ClaimTypes.Email,user.Email!),
            }.Union(userClaims).Union(userRoleClaims);

       

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["AuthKey"] ?? string.Empty));

            var token = new JwtSecurityToken(
                audience: _configuration.GetSection("JWT")["ValidAudience"],
                issuer: _configuration.GetSection("JWT")["ValidIssuer"],
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration.GetSection("JWT")["DurationInMinutes"] ?? "0")),
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256),
                claims :authClaims
                
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

  
    }
}
