using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Store.ViewComponents
{
	public class StoreSidebarViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("StoreSidebar");
		}
	}
}