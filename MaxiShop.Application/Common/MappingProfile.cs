using AutoMapper;
using MaxiShop.Application.DTO.Brand;
using MaxiShop.Application.DTO.Category;
using MaxiShop.Application.DTO.Product;
using MaxiShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ForMember(x=>x.Name, src => src.MapFrom(x=>x.BrandName)).ReverseMap();

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(x => x.Brand, opt => opt.MapFrom(src => src.Brand.BrandName));
        }
    }
}
