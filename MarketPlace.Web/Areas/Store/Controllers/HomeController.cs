using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Store.Controllers
{
    public class HomeController : StoreBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
