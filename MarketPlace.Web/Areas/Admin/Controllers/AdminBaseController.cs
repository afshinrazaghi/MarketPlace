using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminBaseController : Controller
    {
        protected JsonResult JsonResult(JsonResultStatus type, string message, object? data)
        {
            var result = new JsonResult(new { status = type.ToString(), message = message, data = data });
            return result;
        }
    }

    public enum JsonResultStatus
    {
        Success,
        Warning,
        Danger,
        Info
    }
}
