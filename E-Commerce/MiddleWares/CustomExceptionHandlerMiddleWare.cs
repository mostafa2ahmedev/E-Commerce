using Azure;
using E_Commerce.APIs.Controller.Errors;
using E_Commerce.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace E_Commerce.MiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;
        private readonly IWebHostEnvironment _env;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next
            , ILogger<CustomExceptionHandlerMiddleWare> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }



        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                // Logic Executed with the Request

                await _next.Invoke(httpContext);

                // Logic Executed with the Response

                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    var response = new ApiResponse(
                    (int)HttpStatusCode.NotFound,
                    $"The requested endpoint : {httpContext.Request.Path} is not found");

                    await httpContext.Response.WriteAsync(response.ToString());
                }
                else if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized) {
                    var response = new ApiResponse(
                       (int)HttpStatusCode.Unauthorized);

                    await httpContext.Response.WriteAsync(response.ToString());
                }


            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);

                 

                }
                else
                {
                    // Production Mode
                    ///Log Exception Details in Database | File (Text, Json)
               
                }


                await HandleExceptionAsync(httpContext, ex);

            }





        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;
            switch (ex)
            {

                default: // Internal Server error   or any error that is not handled

                    // Development Mode
                    if (_env.IsDevelopment())
                    {
               

                        response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);

                    }
                    else
                    {
                        // Production Mode
                     
                        response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                    }

                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;


                case NotFoundException: // Not Found
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse((int)HttpStatusCode.NotFound, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                 case ValidationException exception: // Bad Request
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiAuthValidationResponse() {Errors = exception.Errors};

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case BadRequestException: // Bad Request
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse((int)HttpStatusCode.BadRequest, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                case UnAuthorizedException: // Bad Request
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse((int)HttpStatusCode.Unauthorized);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
              

            }
        }
    }
}
