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

        public HomeController(ILogger<HomeController> logger, ICaptchaValidator captchaValidator, IContactService contactService)
        {
            _logger = logger;
            _captchaValidator = captchaValidator;
            _contactService = contactService;
        }
        #endregion

        #region index
        public IActionResult Index()
        {
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

    }
}