using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Stores;
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
            filter.Take = 1;
            return View(await _storeService.FilterStore(filter));
        }
        #endregion    
    }
}
