using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.Application.Services.DTO.Order;
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapping
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (source.Product != null && !string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_configuration.GetSection("Urls")["ApiBaseUrl"]}{source.Product.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
