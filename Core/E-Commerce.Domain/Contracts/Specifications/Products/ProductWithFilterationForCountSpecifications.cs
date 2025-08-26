using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts.Specifications.Products
{
    public class ProductWithFiltrationForCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithFiltrationForCountSpecifications(int? brandId, int? categoryId,string? search) :
            base(P =>
                (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                 &&
                 (!brandId.HasValue || P.BrandId == brandId)
                 &&
                 (!categoryId.HasValue || P.CategoryId == categoryId) 

)
        {
        }
    }
}
