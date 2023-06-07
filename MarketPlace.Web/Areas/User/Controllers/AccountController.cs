using GoogleReCaptcha.V3.Interface;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class AccountController : UserBaseController
    {
        #region constructor
        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;
        public AccountController(IUserService userService, ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }
        #endregion

        #region change password
        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("change-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(model.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچا تایید نشد";
                return View();
            }


            var res = await _userService.ChangeUserPassword(model, User.GetUserId()!.Value);
            switch (res)
            {
                case ChangePasswordResult.CurrentPasswordIncorrect:
                    TempData[ErrorMessage] = "کلمه عبور فعلی نادرست می باشد";
                    ModelState.AddModelError("CurrentPassword", "کلمه عبور فعلی نادرست می باشد");
                    break;
                case ChangePasswordResult.UserNotFound:
                    TempData[ErrorMessage] = "اطلاعات وارد شده نادرست می باشد، لطفا مجددا وارد سیستم شوید";
                    break;
                case ChangePasswordResult.UseNewPassword:
                    TempData[ErrorMessage] = "لطفا از کلمه عبور جدیدی استفاده کنید";
                    ModelState.AddModelError("NewPassword", "لطفا از کلمه عبور جدیدی استفاده کنید");
                    break;
                case ChangePasswordResult.Success:
                    TempData[SuccessMessage] = "کلمه عبور با موفقیت تغییر یافت";
                    TempData[InfoMessage] = "لطفا جهت تکمیل فرآیند تغییر کلمه عبور مجددا وارد سیستم شوید";
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("Login", "Account", new { area = "" });
            }

            return View(model);
        }
        #endregion

        #region edit profile
        [HttpGet("edit-profile")]
        public async Task<IActionResult> EditProfile()
        {
            var userProfile = await _userService.GetProfileForEdit(User.GetUserId()!.Value);
            if (userProfile is null) return NotFound();
            return View(userProfile);
        }

        [HttpPost("edit-profile"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileDTO model, IFormFile? avatarImage)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.EditUserProfile(model, User.GetUserId()!.Value, avatarImage);
                switch (res)
                {
                    case EditProfileResult.NotFound:
                        TempData[ErrorMessage] = "کاربری با مشخصات وارد شده یافت نشد، لطفا مجددا وارد سیستم شوید";
                        break;
                    case EditProfileResult.IsBlocked:
                        TempData[ErrorMessage] = "حساب کاربری شما بلاک شده است";
                        break;
                    case EditProfileResult.IsNotActive:
                        TempData[ErrorMessage] = "حساب کاربری شما فعال نشده است";
                        break;
                    case EditProfileResult.Success:
                        TempData[SuccessMessage] = "اطلاعات کاربر با موفقیت ویرایش شد";
                        return RedirectToAction("EditProfile");
                        break;
                }
            }
            return View(model);
        }
        #endregion
    }
}
