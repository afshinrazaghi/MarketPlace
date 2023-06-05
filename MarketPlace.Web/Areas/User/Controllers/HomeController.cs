using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
	public class HomeController : UserBaseController
	{
		#region dashboard
		[HttpGet("")]
		public IActionResult Dashboard()
		{
			return View();
		}
		#endregion
	}
}
