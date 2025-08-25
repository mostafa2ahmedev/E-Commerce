using E_Commerce.Extensions;
using E_Commerce.Persistence;
using Microsoft.Extensions.DependencyInjection;
using E_Commerce.APIs.Controller;
using E_Commerce.Application;

namespace E_Commerce
{
    public class Program
    {




  
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);




            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllers().AddApplicationPart(typeof(APIs.Controller.AssemblyInformation).Assembly);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion




            var app = builder.Build();


            await app.InitializeStoreContextAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
