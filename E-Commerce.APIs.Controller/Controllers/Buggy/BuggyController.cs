using E_Commerce.APIs.Controller.Errors;
using E_Commerce.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace E_Commerce.APIs.Controller.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {


        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest() { 
        
        return NotFound( new ApiResponse(401)); // 404
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
         
                throw new Exception();
       
         
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {

            return BadRequest(new ApiResponse(400)); // 400
        }
        [HttpGet("unauthorized")]
        public IActionResult GetUnAuthorized()
        {

            return Unauthorized(); // 401
        }

        [HttpGet("forbidden")]
        public IActionResult GetForbidden()
        {

            return Forbid(); // 401
        }
        [HttpGet("badrequest/{id}")]
        public IActionResult GetUnValidationError(int id)
        {
    
             
            return Ok(id); // 400
        }
    }
}
