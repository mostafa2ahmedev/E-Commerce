using AutoMapper;
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapping
{
    internal class MappingProfile : Profile
    {

        public MappingProfile() {

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D=>D.Brand,O=>O.MapFrom(S=>S.Brand!.Name))
                .ForMember(D => D.Category, O => O.MapFrom(S => S.Category!.Name));


            CreateMap<ProductBrand, BrandToReturnDto>();

            CreateMap<ProductCategory, CategoryToReturnDto>();
        }
    }
}
