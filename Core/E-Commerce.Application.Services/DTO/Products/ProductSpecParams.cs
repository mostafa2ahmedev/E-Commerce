using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.DTO.Products
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public string? Search { get; set; }


        private const int MaxPageSize = 10;
        private int pageSize;
        public int PageSize { 
            get => pageSize;
            set { 
                pageSize = value > MaxPageSize ? MaxPageSize : value;}
        }
        public int PageIndex { get; set; } = 1;
    }
}
