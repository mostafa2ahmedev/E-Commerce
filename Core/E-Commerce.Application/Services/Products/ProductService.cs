using AutoMapper;
using E_Commerce.Application.Services.Common;
using E_Commerce.Application.Services.Contracts.Products;
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Domain.Contracts.Persistence;
using E_Commerce.Domain.Contracts.Specifications;
using E_Commerce.Domain.Contracts.Specifications.Products;
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
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams SpecParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(
                SpecParams.Sort, SpecParams.BrandId, SpecParams.CategoryId, SpecParams.PageSize, SpecParams.PageIndex,SpecParams.Search
                );
            var products=   await  _unitOfWork.GetRepository<Product,int>().GetAllAsyncWithSpec(spec);
            var productsToReturn = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationForCountSpecifications(
             SpecParams.BrandId, SpecParams.CategoryId
                 );

            var count = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(countSpec);
            return new Pagination<ProductToReturnDto>(SpecParams.PageIndex, SpecParams.PageSize, count) {
                Data = productsToReturn,
            };
        }
        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsyncWithSpec(spec);
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
