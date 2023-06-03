using System.Security.Claims;
using System.Security.Principal;

namespace MarketPlace.Web.PresentationExtensions
{
    public static class HttpExtensions
    {
        public static string GetUserIp(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress!.ToString() ?? "NO IP EXIST";
        }
    }
}
