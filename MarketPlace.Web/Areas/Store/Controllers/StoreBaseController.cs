using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Store.Controllers
{
    [Authorize]
    [Area("Store")]
    public class StoreBaseController : Controller
    {
       
    }
}
