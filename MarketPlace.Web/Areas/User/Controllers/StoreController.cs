using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Stores;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class StoreController : UserBaseController
    {
        #region constructor
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        #endregion

        #region store request panel
        [HttpGet("request-store-panel")]
        public IActionResult RequestStorePanel()
        {
            return View();
        }

        [HttpPost("request-store-panel"), ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestStorePanel(RequestStoreDTO request)
        {
            if (ModelState.IsValid)
            {
                var res = await _storeService.RequestStore(request, User.GetUserId()!.Value);
                switch (res)
                {
                    case RequestStoreResult.HasNoPermission:
                        TempData[ErrorMessage] = "شما دسترسی لازم جهت انجام عملیات مورد نظر را ندارید";
                        break;
                    case RequestStoreResult.HasUnderProgressRequest:
                        TempData[WarningMessage] = "درخواست های قبلی شما در حال پیگیری می باشند ";
                        TempData[InfoMessage] = "در حال حاضر نمی توانید درخواست جدید ثبت کنید";
                        break;
                    case RequestStoreResult.Success:
                        TempData[SuccessMessage] = "درخواست شما با موفقیت ثبت شد";
                        TempData[InfoMessage] = "فرآیند تایید اطلاعات شما در حال پیگیری می باشد ";
                        // todo: redirect to list of requests 
                        return RedirectToAction("StoreRequests");
                }
            }

            return View(request);
        }

        #endregion

        #region store requests
        [HttpGet("store-requests")]
        public async Task<IActionResult> StoreRequests(FilterStoreDTO filter)
        {
            filter.Take = 5;
            filter.UserId = User.GetUserId()!.Value;
            filter.StoreState = FilterStoreState.All;
            var res = await _storeService.FilterStore(filter);
            return View(res);
        }
        #endregion
    }
}
