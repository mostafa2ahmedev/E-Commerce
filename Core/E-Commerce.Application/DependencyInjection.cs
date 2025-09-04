using AutoMapper;
using E_Commerce.Application.Mapping;
using E_Commerce.Application.Services;
using E_Commerce.Application.Services.Basket;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.Contracts.Basket;
using E_Commerce.Application.Services.Contracts.Products;
using E_Commerce.Application.Services.Products;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

            //services.AddAutoMapper(Mapper => Mapper.AddProfile(new MappingProfile()));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            services.AddAutoMapper(mapper => mapper.AddProfile(new MappingProfile()));
            services.AddScoped(typeof(ProductPictureUrlResolver));
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            services.AddScoped(typeof(IBasketService), typeof(BasketService));

            //services.AddScoped(typeof(Func<IBasketService>), typeof(Func<BasketService>));

            services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            {
            return () => serviceProvider.GetRequiredService<IBasketService>();
             });

            return services;

        }
    }
}
