using MarketPlace.DataLayer.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
	public class AccountController : UserBaseController
	{
		[Route("change-password")]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
		{
			await Task.CompletedTask;
			return View();
		}
	}
}
