using E_Commerce.Extensions;
using E_Commerce.Persistence;
using Microsoft.Extensions.DependencyInjection;
using E_Commerce.APIs.Controller;
using E_Commerce.Application;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.APIs.Controller.Errors;
using E_Commerce.MiddleWares;
using E_Commerce.Infrastructure;

namespace E_Commerce
{
    public class Program
    {




  
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);




            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => { 
            options.SuppressModelStateInvalidFilter = false;
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    //var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0).SelectMany(P => P.Value!.Errors)
                    //         .Select(E => E.ErrorMessage);

                    var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                             .Select(P => new ApiValidationErrorResponse.ValidationError()
                                             {
                                                 Field = P.Key,
                                                 Errors = P.Value!.Errors.Select(E => E.ErrorMessage)

                                             });
                    return new BadRequestObjectResult(new ApiValidationErrorResponse()
                    {
                        Errors = errors

                    });
                };
            
            }).AddApplicationPart(typeof(APIs.Controller.AssemblyInformation).Assembly);



            builder.Services.Configure<ApiBehaviorOptions>(builder.Configuration);

            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion




            var app = builder.Build();


            await app.InitializeStoreContextAsync();

            #region Configure MiddleWares
            // Configure the HTTP request pipeline.

            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            //app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion
            app.Run();
        }
    }
}
