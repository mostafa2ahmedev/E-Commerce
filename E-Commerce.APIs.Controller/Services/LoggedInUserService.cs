using E_Commerce.Application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIs.Controller.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
        }
        public string? UserId { get; }

    }
}
