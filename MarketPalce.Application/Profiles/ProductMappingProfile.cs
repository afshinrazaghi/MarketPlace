using AutoMapper;
using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductDTO, Product>();
        }
    }
}
