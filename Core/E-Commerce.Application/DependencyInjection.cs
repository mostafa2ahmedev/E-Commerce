using AutoMapper;
using E_Commerce.Application.Mapping;
using E_Commerce.Application.Services;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.Contracts.Products;
using E_Commerce.Application.Services.Products;
using E_Commerce.Domain.Contracts;

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

            return services;

        }
    }
}
