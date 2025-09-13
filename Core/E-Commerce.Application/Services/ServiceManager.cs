using AutoMapper;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.Contracts.Authentication;

using E_Commerce.Application.Services.Contracts.Products;
using E_Commerce.Application.Services.Authentication;

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
using E_Commerce.Application.Services.Contracts.Order;

namespace E_Commerce.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;

        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IOrderService> _orderService;
        public IProductService ProductService => _productService.Value;


        public IAuthService AuthService => _authService.Value;

        public IOrderService OrderService => _orderService.Value;

        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,IConfiguration configuration,
             Func<IAuthService> authServiceFactory,
               Func<IOrderService> OrderServiceFactory)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _authService = new Lazy<IAuthService>(authServiceFactory,true);
            _orderService = new Lazy<IOrderService>(OrderServiceFactory, true);
        }
    }
}
