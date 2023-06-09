﻿using MarketPlace.DataLayer.DTOs.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IContactService : IAsyncDisposable
    {
        #region constact us
        Task CreateContactUs(CreateContactUsDTO model, long? userId, string userIp);
        #endregion

        #region ticket
        Task<CreateTicketResult> CreateUserTicket(CreateTicketDTO model, long userId);
        #endregion
    }
}
