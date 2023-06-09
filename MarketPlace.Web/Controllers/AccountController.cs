﻿using GoogleReCaptcha.V3.Interface;
using MarketPlace.Application.Services;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketPlace.Web.Controllers
{
    public class AccountController : SiteBaseController
    {
        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;


        public AccountController(IUserService userService, ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }

        #region register
        [HttpGet("register")]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
                return Redirect("/");
            return View();
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(model.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچا تایید نشد";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var res = await _userService.RegisterUser(model);
                switch (res)
                {
                    case RegisterUserResult.MobileExists:
                        TempData[ErrorMessage] = "تلفن همراه وارد شده تکراری می باشد";
                        ModelState.AddModelError("Mobile", "تلفن همراه وارد شده تکراری می باشد");
                        break;
                    case RegisterUserResult.Success:
                        TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد";
                        TempData[InfoMessage] = "کد تایید تلفن همراه برای شما ارسال شد";
                        return RedirectToAction("ActivateMobile", new { Mobile = model.Mobile });
                };
            }

            return View(model);
        }
        #endregion

        #region login
        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
                return Redirect("/");
            return View();
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            if (User.Identity!.IsAuthenticated) { return Redirect("/"); }

            if (!await _captchaValidator.IsCaptchaPassedAsync(model.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچا تایید نشد";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var loginResult = await _userService.LoginUser(model);
                switch (loginResult)
                {
                    case LoginUserResult.NotFound:
                        TempData[ErrorMessage] = "کاربر یافت نشد";
                        break;
                    case LoginUserResult.NotActivated:
                        TempData[WarningMessage] = "حساب کاربری شما فعال نشده است";
                        break;
                    case LoginUserResult.Success:

                        var user = (await _userService.GetUserByMobile(model.Mobile))!;
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.GivenName, user.FirstName + " "+ user.LastName),
                            new Claim(ClaimTypes.Name, user.Mobile),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties()
                        {
                            IsPersistent = model.RememberMe
                        };

                        await HttpContext.SignInAsync(principal, properties);
                        TempData[SuccessMessage] = "عملیات ورود با موفقیت انجام شد";
                        return Redirect("/");

                }
            }
            return View(model);
        }
        #endregion

        #region mobile activation
        [HttpGet("activate-mobile/{mobile}")]
        public IActionResult ActivateMobile(string mobile)
        {
            var model = new ActivateMobileDTO { Mobile = mobile };
            return View(model);
        }

        [HttpPost("activate-mobile/{mobile}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateMobile(ActivateMobileDTO model)
        {
            if (User.Identity!.IsAuthenticated) { return Redirect("/"); }

            if (!await _captchaValidator.IsCaptchaPassedAsync(model.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچا تایید نشد";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var res = await _userService.ActivateMobile(model);
                if (res)
                {
                    TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال گردید";
                    return RedirectToAction("Login");
                }
                TempData[ErrorMessage] = "کاربری با مشخصات وارد شده یافت نشد";
            }

            return View(model);
        }
        #endregion

        #region logout
        [HttpGet("log-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        #endregion

        #region forget-password
        [HttpGet("forgot-pass")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-pass"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(model.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچا تایید نشد";
                return View(model);
            }

            var res = await _userService.RecoverPassword(model);
            switch (res)
            {
                case ForgotPasswordResult.NotFound:
                    TempData[WarningMessage] = "کاربر یافت نشد";
                    break;
                case ForgotPasswordResult.Success:
                    TempData[SuccessMessage] = "کلمه عبور جدید برای شما ارسال شد";
                    TempData[InfoMessage] = "لطفا پس از ورود به حساب کاربری، کلمه عبور خود را تغییر دهید";
                    return Redirect("/login");
            }

            return View(model);
        }
        #endregion
    }
}
