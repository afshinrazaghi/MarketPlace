using MarketPlace.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISiteSettingService:IAsyncDisposable
    {
        public Task<SiteSetting?> GetDefaultSiteSetting();
    }
}
