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
        public ContactService(IGenericRepository<ContactUs> genericRepository)
        {
            _contactUsRepository = genericRepository;
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

        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _contactUsRepository.DisposeAsync();
        }
        #endregion
    }
}
