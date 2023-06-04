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
    public class SiteService : ISiteService
    {
        #region constructor
        private readonly IGenericRepository<SiteSetting> _siteSettingRepository;
        private readonly IGenericRepository<Slider> _sliderRepository;

        public SiteService(IGenericRepository<SiteSetting> siteSettingRepository, IGenericRepository<Slider> sliderRepository)
        {
            _siteSettingRepository = siteSettingRepository;
            _sliderRepository = sliderRepository;
        }
        #endregion

        #region site settings
        public async Task<SiteSetting?> GetDefaultSiteSetting()
        {
            return await _siteSettingRepository.GetQuery().AsNoTracking().SingleOrDefaultAsync(site => site.IsDefault && !site.IsDelete);

        }
        #endregion

        #region slider

        public async Task<List<Slider>> GetAllActiveSliders()
        {
            return await _sliderRepository.GetQuery().AsNoTracking().Where(s => s.IsActive && !s.IsDelete).ToListAsync();
        }
        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            if (_siteSettingRepository is not null)
                await _siteSettingRepository.DisposeAsync();

            if (_sliderRepository is not null)
                await _sliderRepository.DisposeAsync();
        }

        #endregion
    }
}
