using AutoMapper;
using E_Commerce.Application.Services.Authentication;
using E_Commerce.Application.Services.Basket;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.Contracts.Authentication;
using E_Commerce.Application.Services.Contracts.Basket;
using E_Commerce.Application.Services.Contracts.Products;
using E_Commerce.Application.Services.Products;
using E_Commerce.Domain.Contracts.Infrastructure;
using E_Commerce.Domain.Contracts.Persistence;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        public readonly Lazy<IProductService> _productService;
        public readonly Lazy<IBasketService> _basketService;
        public readonly Lazy<IAuthService> _authService;
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _authService.Value;
        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,IConfiguration configuration
            , Func<IBasketService> basketServiceFactory,
             Func<IAuthService> authServiceFactory)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory,true);
            _authService = new Lazy<IAuthService>(authServiceFactory,true);
        }
    }
}
