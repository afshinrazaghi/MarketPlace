using MarketPlace.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.ViewComponents
{
    #region site header
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly ISiteSettingService _siteSettingService;

        public SiteHeaderViewComponent(ISiteSettingService siteSettingService)
        {
            _siteSettingService = siteSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.SiteSetting =await _siteSettingService.GetDefaultSiteSetting();

            return View("SiteHeader");
        }
    }
    #endregion


    #region site footer
    public class SiteFooterViewComponent : ViewComponent
    {
        private readonly ISiteSettingService _siteSettingService;

        public SiteFooterViewComponent(ISiteSettingService siteSettingService)
        {
            _siteSettingService = siteSettingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.SiteSetting = await _siteSettingService.GetDefaultSiteSetting();
            return View("SiteFooter");
        }
    } 
    #endregion
}
