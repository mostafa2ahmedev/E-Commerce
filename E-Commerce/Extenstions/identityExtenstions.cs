using E_Commerce.Application.Services.Authentication;
using E_Commerce.Application.Services.Contracts.Authentication;
using E_Commerce.Domain.Contracts.Persistence.DbInitializer;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Persistence.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

namespace E_Commerce.Extensions
{
    public static class IdentityExtensions
    {

        public static  IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //User
                options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = "";

                //SignIn
                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;

                //Password
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 2;

                //LockOut
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);


            }).AddEntityFrameworkStores<StoreIdentityDbContext>();



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
            {
               
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("JWT")["ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("JWT")["ValidIssuer"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(0),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT")["AuthKey"] ?? string.Empty)),
                };


            });


            //services.AddScoped(typeof(Func<IAuthService>), typeof(Func<AuthService>));
            services.AddScoped(typeof(IAuthService),typeof(AuthService));
            services.AddScoped(typeof(Func<IAuthService>), serviceProvider => { 
            return () => serviceProvider.GetRequiredService<IAuthService>();
            
            });
            return services;
        }
    }
}
