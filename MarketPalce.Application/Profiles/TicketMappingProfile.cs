using AutoMapper;
using MarketPlace.DataLayer.DTOs.Contacts;
using MarketPlace.DataLayer.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Profiles
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<CreateTicketDTO, Ticket>()
                .ForMember(destination => destination.TicketState, opt => opt.MapFrom(src => TicketState.UnderProgress))
                .ForMember(destination => destination.IsReadByOwner, opt => opt.MapFrom(src => true));
            CreateMap<CreateTicketDTO, TicketMessage>();
            CreateMap<Ticket, TicketDetailDTO>().ForMember(destination => destination.Ticket, opt => opt.MapFrom(src => src));
        }
    }
}
