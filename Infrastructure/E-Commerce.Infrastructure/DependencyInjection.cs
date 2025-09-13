using E_Commerce.Application.Services.Common.Contracts.Infrastructure;
using E_Commerce.Domain.Contracts.Infrastructure;

using E_Commerce.Infrastructure.BasketRepositoryy;
using E_Commerce.Infrastructure.BasketServices;
using E_Commerce.Infrastructure.CacheServices;
using E_Commerce.Infrastructure.PaymentServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) => {
                return  ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            
            });
            services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            services.AddSingleton(typeof(IResponseCacheService), typeof(CacheService));
            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            return services;

        }
    }
}
