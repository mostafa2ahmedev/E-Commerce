using E_Commerce.Domain.Contracts.Infrastructure;
using E_Commerce.Domain.Contracts.Persistence;
using E_Commerce.Domain.Contracts.Persistence.DbInitializer;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Persistence.Data;
using E_Commerce.Persistence.Identity;
using E_Commerce.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace E_Commerce.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options => {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("StoreContext"));

            });
            services.AddDbContext<StoreIdentityDbContext>(options => {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("IdentityContext"));

            });

       

            //services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));
            services.AddScoped(typeof(IStoreIdentityInitializer), typeof(StoreIdentityInitializer));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
  
            return services;

        }
    }
}