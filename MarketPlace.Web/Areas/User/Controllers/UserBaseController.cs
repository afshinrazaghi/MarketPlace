using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    [Authorize]
	[Area("User")]
	[Route("user")]
	public class UserBaseController : Controller
    {
        protected const string SuccessMessage = "SuccessMessage";
        protected const string InfoMessage = "InfoMessage";
        protected const string WarningMessage = "WarningMessage";
        protected const string ErrorMessage = "ErrorMessage";
    }
}
