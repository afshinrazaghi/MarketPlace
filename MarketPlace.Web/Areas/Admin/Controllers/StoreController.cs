using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Common;
using MarketPlace.DataLayer.DTOs.Stores;
using MarketPlace.Web.Models;
using MarketPlace.Web.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    public class StoreController : AdminBaseController
    {
        #region constructor
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        #endregion

        #region store-requests
        [HttpGet("store-requests")]
        public async Task<IActionResult> StoreRequests(FilterStoreDTO filter)
        {
            filter.UserId = User.GetUserId()!.Value;
            return View(await _storeService.FilterStore(filter));
        }
        #endregion

        #region accept store request
        [HttpPost("accept-store-request")]
        public async Task<IActionResult> AcceptStoreRequest(long requestId)
        {
            var res = await _storeService.AcceptStoreRequest(requestId);
            if (res)
            {
                return JsonResult(JsonResultStatus.Success, "درخواست مورد نظر با موفقیت تایید شد", null);
            }
            else
            {
                return JsonResult(JsonResultStatus.Danger, "اطلاعاتی با این مشخصات یافت نشد", null);
            }
        }
        #endregion

        #region reject store request
        [HttpPost("reject-store-request")]
        public async Task<IActionResult> RejectStoreRequest(RejectItemDTO rejectItem)
        {
            var res = await _storeService.RejectStoreRequest(rejectItem);
            if (res)
            {
                return JsonResult(JsonResultStatus.Success, "درخواست مورد نظر با موفقیت رد شد", rejectItem);
            }
            else
            {
                return JsonResult(JsonResultStatus.Danger, "اطلاعاتی با این مشخصات یافت نشد", null);
            }
        }
        #endregion
    }
}
