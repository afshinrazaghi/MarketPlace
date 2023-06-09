﻿using AutoMapper;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Contacts;
using MarketPlace.DataLayer.DTOs.Paging;
using MarketPlace.DataLayer.Entities.Contacts;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
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
        private readonly IGenericRepository<TicketMessage> _ticketMessageRepository;
        private readonly IMapper _mapper;
        public ContactService(IGenericRepository<ContactUs> genericRepository, IMapper mapper, IGenericRepository<Ticket> ticketRepository, IGenericRepository<TicketMessage> ticketMessageRepository)
        {
            _contactUsRepository = genericRepository;
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _ticketMessageRepository = ticketMessageRepository;
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

            ticket.TicketMessages = new List<TicketMessage> { ticketMessage };


            ticketMessage.SenderId = userId;
            await _ticketRepository.AddEntity(ticket);
            await _ticketRepository.SaveChanges();
            return CreateTicketResult.Success;
        }

        public async Task<FilterTicketDTO> FilterTicket(FilterTicketDTO filter)
        {
            var query = _ticketRepository.GetQuery();

            #region filter
            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(t => EF.Functions.Like(t.Title, $"%{filter.Title}%"));

            if (filter.UserId.HasValue)
                query = query.Where(t => t.OwnerId == filter.UserId);
            #endregion

            #region state
            switch (filter.FilterTicketState)
            {
                case FilterTicketState.All:
                    break;
                case FilterTicketState.NotDeleted:
                    query = query.Where(t => !t.IsDelete);
                    break;
                case FilterTicketState.Deleted:
                    query = query.Where(t => t.IsDelete);
                    break;
            }

            if (filter.TicketSection.HasValue)
                query = query.Where(t => t.TicketSection == filter.TicketSection.Value);

            if (filter.TicketPriority.HasValue)
                query = query.Where(t => t.TicketPriority == filter.TicketPriority.Value);


            #endregion

            #region ordering
            switch (filter.FilterTicketOrder)
            {
                case FilterTicketOrder.CreateDate_ASC:
                    query = query.OrderBy(t => t.CreateDate);
                    break;
                case FilterTicketOrder.CreateDate_DESC:
                    query = query.OrderByDescending(t => t.CreateDate);
                    break;
            }
            #endregion

            #region paging
            var basePaging = Pager.Build(filter.CurrentPage, await query.CountAsync(), filter.Take, filter.HowManyBeforeAndAfter);
            var tickets = await query.Paging(basePaging).ToListAsync();
            #endregion

            return filter.SetTickets(tickets).SetPaging(basePaging);
        }

        public async Task<TicketDetailDTO> GetTicketForShow(long ticketId, long userId)
        {
            var ticket = await _ticketRepository.GetQuery().Include(t => t.Owner).Include(t => t.TicketMessages.OrderByDescending(tm => tm.CreateDate))
                .AsNoTracking()
                .SingleOrDefaultAsync(t => !t.IsDelete && t.Id == ticketId && t.OwnerId == userId);
            return _mapper.Map<TicketDetailDTO>(ticket);
        }
        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _contactUsRepository.DisposeAsync();
        }

        public async Task<AnswerTicketResult> AnswerTicket(AnswerTicketDTO answerTicket, long userId)
        {
            var ticket = await _ticketRepository.GetEntityById(answerTicket.Id);
            if (ticket == null) return AnswerTicketResult.NotFound;
            if (ticket.OwnerId != userId) return AnswerTicketResult.NotForUser;
            var newTicketMessage = new TicketMessage
            {
                TicketId = answerTicket.Id,
                Text = answerTicket.Text,
                SenderId = userId,
            };
            await _ticketMessageRepository.AddEntity(newTicketMessage);

            ticket.IsReadByOwner = true;
            ticket.IsReadByAdmin = false;

            await _ticketMessageRepository.SaveChanges();

            return AnswerTicketResult.Success;
        }
        #endregion
    }
}
