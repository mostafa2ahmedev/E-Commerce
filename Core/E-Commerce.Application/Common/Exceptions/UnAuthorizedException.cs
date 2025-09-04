using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common.Exceptions
{
    public class UnAuthorizedException : ApplicationException
    {

        public UnAuthorizedException(string? message ="Invalid Login") :base(message)
        {
            
        }
    }
}
