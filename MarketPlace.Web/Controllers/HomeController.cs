using GoogleReCaptcha.V3.Interface;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Contacts;
using MarketPlace.Web.Models;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketPlace.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        #region constrcutor
        private readonly ILogger<HomeController> _logger;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IContactService _contactService;
        private readonly ISiteService _siteService;

		public HomeController(ILogger<HomeController> logger, ICaptchaValidator captchaValidator, IContactService contactService, ISiteService siteService)
		{
			_logger = logger;
			_captchaValidator = captchaValidator;
			_contactService = contactService;
			_siteService = siteService;
		}
		#endregion

		#region index
		public async Task<IActionResult> Index()
        {
            ViewBag.banners =await _siteService.GetSiteBannersByPlacement(new List<DataLayer.Entities.Site.BannerPlacement>
            {
                DataLayer.Entities.Site.BannerPlacement.Home_1,
                DataLayer.Entities.Site.BannerPlacement.Home_2,
                DataLayer.Entities.Site.BannerPlacement.Home_3
            });
            return View();
        }
        #endregion

        #region contacts
        [HttpGet("contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost("contact-us"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(CreateContactUsDTO model)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(model.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچا تایید نشد";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var userIp = HttpContext.GetUserIp();
                var userId = User.GetUserId();
                await _contactService.CreateContactUs(model, userId, userIp);
                TempData[SuccessMessage] = "پیام شما با موفقیت ارسال شد";
                return RedirectToAction("ContactUs");
            }

            return View(model);
        }

        #endregion



        #region about us

        [HttpGet("about-us")]
        public async Task<IActionResult> AboutUs()
        {
            var siteSetting = await _siteService.GetDefaultSiteSetting();
            return View(siteSetting);
        }
        #endregion

    }
}