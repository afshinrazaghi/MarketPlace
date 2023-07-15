using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Store.Controllers
{
    [Authorize]
    [Area("Store")]
    [Route("Store")]
    public class StoreBaseController : Controller
    {
        protected JsonResult JsonResult(JsonResultStatus type, string message, object? data)
        {
            var result = new JsonResult(new { status = type.ToString(), message = message, data = data });
            return result;
        }

        protected const string SuccessMessage = "SuccessMessage";
        protected const string InfoMessage = "InfoMessage";
        protected const string WarningMessage = "WarningMessage";
        protected const string ErrorMessage = "ErrorMessage";
    }

    public enum JsonResultStatus
    {
        Success,
        Warning,
        Danger,
        Info
    }


}
