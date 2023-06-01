using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.Entities.Site;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class SiteSettingService : ISiteSettingService
    {
        #region constructor
        private readonly IGenericRepository<SiteSetting> _siteSettingRepository;

        public SiteSettingService(IGenericRepository<SiteSetting> siteSettingRepository)
        {
            _siteSettingRepository = siteSettingRepository;
        } 
        #endregion

        public async Task<SiteSetting?> GetDefaultSiteSetting()
        {
            return await _siteSettingRepository.GetQuery().SingleOrDefaultAsync(site => site.IsDefault && !site.IsDelete);
        }

        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _siteSettingRepository.DisposeAsync();
        } 
        #endregion
    }
}
