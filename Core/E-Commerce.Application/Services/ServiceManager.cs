using AutoMapper;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.Contracts.Products;
using E_Commerce.Application.Services.Products;
using E_Commerce.Domain.Contracts;
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
        public IProductService ProductService => _productService.Value;


        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        }
    }
}
