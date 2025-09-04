using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common.Exceptions
{
    public class ValidationException : BadRequestException
    {
        public IEnumerable<string> Errors { get; set; } = null!;



        public ValidationException(string message = "Validation Exception"):base(message)
        {
            
        }


    }
 
}
