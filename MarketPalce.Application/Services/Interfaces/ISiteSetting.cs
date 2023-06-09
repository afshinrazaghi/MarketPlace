﻿using MarketPlace.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISiteService : IAsyncDisposable
    {
        #region site settings
        public Task<SiteSetting?> GetDefaultSiteSetting();
        #endregion

        #region slider

        public Task<List<Slider>> GetAllActiveSliders();
        #endregion

        #region site banners
        public Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> bannerPlacements);
        #endregion
    }
}
