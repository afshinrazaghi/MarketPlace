using MarketPlace.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.ViewComponents
{
    #region site header
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly ISiteService _siteSettingService;

        public SiteHeaderViewComponent(ISiteService siteSettingService)
        {
            _siteSettingService = siteSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.SiteSetting = await _siteSettingService.GetDefaultSiteSetting();

            return View("SiteHeader");
        }
    }
    #endregion

    #region site footer
    public class SiteFooterViewComponent : ViewComponent
    {
        private readonly ISiteService _siteSettingService;

        public SiteFooterViewComponent(ISiteService siteSettingService)
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

    #region home slider

    public class HomeSliderViewComponent : ViewComponent
    {
        #region constructor
        private readonly ISiteService _siteService;
        public HomeSliderViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }
        #endregion

        #region invoke
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _siteService.GetAllActiveSliders();
            return View("HomeSlider", sliders);
        }
        #endregion
    }
    #endregion
}
