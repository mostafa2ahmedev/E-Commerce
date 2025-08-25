using AutoMapper;
using E_Commerce.Application.Services.Contracts.Products;
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Products
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        {
            var products=   await  _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            var productsToReturn = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return productsToReturn;
        }
        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var productToReturn = _mapper.Map<ProductToReturnDto>(product);
            return productToReturn;
        }
        public async Task<IEnumerable<BrandToReturnDto>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsToReturn = _mapper.Map<IEnumerable<BrandToReturnDto>>(brands);
            return brandsToReturn;
        }

        public async Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryToReturnDto>>(categories);
            return categoriesToReturn;
        }

       

    }
}
