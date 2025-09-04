using E_Commerce.Domain.Contracts.Persistence.DbInitializer;

namespace E_Commerce.Extensions
{
    public static class InitializerExtensions
    {

        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app) {

            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;

            var storeContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
            var storeIdentityContextInitializer = services.GetRequiredService<IStoreIdentityInitializer>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();



            try
            {
                await storeContextInitializer.InitializeAsync();
                await storeContextInitializer.SeedAsync();


                await storeIdentityContextInitializer.InitializeAsync();
                await storeIdentityContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "An error has been occurred during applying the migrations.");

            }

            return app;
        }
    }
}
