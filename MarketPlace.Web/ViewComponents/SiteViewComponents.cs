using AutoMapper;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.ViewComponents
{
    #region site header
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly ISiteService _siteSettingService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SiteHeaderViewComponent(ISiteService siteSettingService, IUserService userService, IMapper mapper)
        {
            _siteSettingService = siteSettingService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.SiteSetting = await _siteSettingService.GetDefaultSiteSetting();
            ViewBag.User = null;
            if (User.Identity!.IsAuthenticated)
            {
                var user = await _userService.GetUserByMobile(User.Identity.Name!);
                ViewBag.user = _mapper.Map<UserSummaryDTO>(user);
            }

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
