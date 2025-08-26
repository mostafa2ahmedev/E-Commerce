using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapping
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string?>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public string? Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration.GetSection("Urls")["ApiBaseUrl"]}{source.PictureUrl}";

            return string.Empty;
        }
    }
}
