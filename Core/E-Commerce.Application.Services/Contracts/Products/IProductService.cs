
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Application.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Contracts.Products
{
    public interface IProductService
    {
        Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams productSpecParams);

        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandToReturnDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync();
    }
}
