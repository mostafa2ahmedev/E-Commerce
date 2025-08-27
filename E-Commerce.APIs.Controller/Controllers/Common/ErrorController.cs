using E_Commerce.APIs.Controller.Errors;
using E_Commerce.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIs.Controller.Controllers.Common
{
    [Route("Errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =false)]
    public class ErrorController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Errors(int code) {
            if (Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiResponse(
                (int)HttpStatusCode.NotFound,
                $"The requested endpoint : {Request.Path} is not found");

             
            }
            else if (Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                var response = new ApiResponse(
                   (int)HttpStatusCode.Unauthorized);

                return Unauthorized();
            }

            return StatusCode(code);
        }
    }
}
