using E_Commerce.Application.Services.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Contracts.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductToReturnDto>> GetProductsAsync();

        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandToReturnDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync();
    }
}
