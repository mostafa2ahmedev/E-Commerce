using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIs.Controller.Errors
{
    public class ApiAuthValidationResponse : ApiResponse
    {
        public required IEnumerable<string> Errors { get; set; }
        public ApiAuthValidationResponse( string? message = null) : base(400, message)
        {
        
        }
    }
}
