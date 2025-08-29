using E_Commerce.Domain.Contracts.Infrastructure;

using E_Commerce.Infrastructure.BasketRepositoryy;
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
            return services;

        }
    }
}
