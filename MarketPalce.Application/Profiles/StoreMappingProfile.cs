using AutoMapper;
using MarketPlace.DataLayer.DTOs.Stores;
using MarketPlace.DataLayer.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Profiles
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<Store, EditRequestStoreDTO>();
        }
    }
}
