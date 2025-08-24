using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration) {
            services.AddDbContext<StoreDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("StoreContext"));

            });
            //services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped(typeof(IStoreContextInitializer),typeof(StoreContextInitializer));
            return services;

        }
    }
}
