using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIs.Controller.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public required IEnumerable<ValidationError> Errors {get;set;}


        public ApiValidationErrorResponse(string? Message = null) : base(400, Message)
        {
        }



        public class ValidationError
        {
            public required string Field { get; set; }
            public required IEnumerable<string> Errors { get; set; }

        }


    }

  
}
