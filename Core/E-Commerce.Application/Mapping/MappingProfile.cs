using AutoMapper;
using E_Commerce.Application.Services.DTO.Common;
using E_Commerce.Application.Services.DTO.Order;
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Domain.Entities.Basket;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAddress = E_Commerce.Domain.Entities.Identity.Address;
using OrderAddress = E_Commerce.Domain.Entities.Orders.Address;
using E_Commerce.Shared.DTO.Basket;
namespace E_Commerce.Application.Mapping
{
    internal class MappingProfile : Profile
    {

        public MappingProfile() {

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D=>D.Brand,O=>O.MapFrom(S=>S.Brand!.Name))
                .ForMember(D => D.Category, O => O.MapFrom(S => S.Category!.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>()
                );


            CreateMap<ProductBrand, BrandToReturnDto>();

            CreateMap<ProductCategory, CategoryToReturnDto>();


            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
              .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                 .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                 .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => $"https://localhost:7123/{src.Product.PictureUrl}"));

            CreateMap<OrderAddress, AddressDto>().ReverseMap();
            CreateMap<UserAddress, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
