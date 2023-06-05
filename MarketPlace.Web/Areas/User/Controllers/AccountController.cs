using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    public class AccountController : UserBaseController
    {
        [Area("User")]
        [Route("user")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
