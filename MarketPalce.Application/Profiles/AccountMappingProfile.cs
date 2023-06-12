using AutoMapper;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Profiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<User, UserSummaryDTO>()
                .ForMember(destination => destination.FullName, opt => opt.MapFrom(src => (!string.IsNullOrEmpty(src.FirstName) && !string.IsNullOrEmpty(src.LastName)) ? src.FirstName + " " + src.LastName : src.Mobile));

        }
    }
}
