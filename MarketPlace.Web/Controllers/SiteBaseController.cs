using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers
{
	public class SiteBaseController : Controller
	{
		#region messages
		public string SuccessMessage = "SuccessMessage";
		public string ErrorMessage = "ErrorMessage";
		public string WarningMessage = "WarningMessage";
		public string InfoMessage = "InfoMessage";
		#endregion
	}
}
