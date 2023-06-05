using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.User.Controllers
{
    [Authorize]
    public class UserBaseController : Controller
    {
        
    }
}
