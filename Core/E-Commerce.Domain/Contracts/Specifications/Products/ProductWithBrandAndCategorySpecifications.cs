using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>

    {


        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId,int pageSize,int pageIndex,string? search)
            : base(
                 P =>
                 (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                 &&
                 (!brandId.HasValue || P.BrandId == brandId) // true
                 &&
                 (!categoryId.HasValue || P.CategoryId == categoryId) // true

                 )
        {
            AddIncludes();
      

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(P => P.Name);
                        break;
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else {
                AddOrderBy(P => P.Name); 
            }

                ApplyPagination((pageIndex - 1) * pageSize, pageSize);
            
            
     
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();

        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

    }
}
