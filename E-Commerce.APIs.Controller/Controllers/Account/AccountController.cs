using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.DTO.Authentication;
using E_Commerce.Application.Services.DTO.Common;
using E_Commerce.Controllers;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var user = await _serviceManager.AuthService.GetCurrentUser(User);
            return Ok(user);


        }
        [HttpGet("address")]
        [Authorize]

        public async Task<ActionResult<UserResultDto>> GetUserAddress()
        {
            var address = await _serviceManager.AuthService.GetUserAddress(User);
            return Ok(address);


        }
        [HttpPut("address")]
        [Authorize]

        public async Task<ActionResult<UserResultDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var address = await _serviceManager.AuthService.UpdateUserAddress(User,addressDto);
            return Ok(address);


        }
        [HttpGet("emailexists")]
    

        public async Task<ActionResult<UserResultDto>> CheckEmailExist(string email)
        {
            return Ok(await _serviceManager.AuthService.EmailExists(email!));


        }
    }
}
