using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.DTO.Authentication;
using E_Commerce.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce.APIs.Controller.Controllers.Account
{
    public class AccountController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("Login")]

        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto) {
        
            var user = await _serviceManager.AuthService.Login(loginDto);
            return Ok(user);
        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserResultDto>> Login(RegisterDto registerDto) {
            var user = await _serviceManager.AuthService.Register(registerDto);
            return Ok(user);

      
        }
    }
}
