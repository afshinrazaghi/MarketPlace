using AutoMapper;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Contacts;
using MarketPlace.DataLayer.Entities.Contacts;
using MarketPlace.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class ContactService : IContactService
    {
        #region constructor
        private readonly IGenericRepository<ContactUs> _contactUsRepository;
        private readonly IGenericRepository<Ticket> _ticketRepository;
        private readonly IMapper _mapper;
        public ContactService(IGenericRepository<ContactUs> genericRepository, IMapper mapper, IGenericRepository<Ticket> ticketRepository)
        {
            _contactUsRepository = genericRepository;
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }
        #endregion

        #region contact us
        public async Task CreateContactUs(CreateContactUsDTO model, long? userId, string userIp)
        {
            var contactUs = new ContactUs
            {
                UserId = userId,
                UserIp = userIp,
                Email = model.Email,
                FullName = model.FullName,
                Subject = model.Subject,
                Text = model.Text
            };

            await _contactUsRepository.AddEntity(contactUs);
            await _contactUsRepository.SaveChanges();
        }

        #endregion

        #region ticket
        public async Task<CreateTicketResult> CreateUserTicket(CreateTicketDTO model, long userId)
        {
            if (string.IsNullOrEmpty(model.Text))
                return CreateTicketResult.Error;

            var ticket = _mapper.Map<Ticket>(model);
            ticket.OwnerId = userId;

            var ticketMessage = _mapper.Map<TicketMessage>(model);
            ticketMessage.SenderId = userId;

            ticket.TickerMessages = new List<TicketMessage> { ticketMessage };


            ticketMessage.SenderId = userId;
            await _ticketRepository.AddEntity(ticket);
            await _ticketRepository.SaveChanges();
            return CreateTicketResult.Success;
        }
        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _contactUsRepository.DisposeAsync();
        }
        #endregion
    }
}
