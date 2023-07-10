using AutoMapper;
using MarketPlace.DataLayer.DTOs.JsTree;
using MarketPlace.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Profiles
{
    public class ProductCategoryMappingProfile:Profile
    {
        public ProductCategoryMappingProfile()
        {
            CreateMap<ProductCategory, JsTreeDTO>()
                .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(destination => destination.Parent, opt => opt.MapFrom(src => !src.ParentId.HasValue ? "#" : src.ParentId.Value.ToString()))
                .ForMember(destination => destination.Text, opt => opt.MapFrom(src => src.Title));
        }
    }
}
