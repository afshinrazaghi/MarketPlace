using System.Security.Claims;
using System.Security.Principal;

namespace MarketPlace.Web.PresentationExtensions
{
    public static class IdentityExtensions
    {
        public static long? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId is not null)
                return Convert.ToInt64(userId);
            return null;
        }

        public static long? GetUserId(this IPrincipal principal)
        {
            var claimsPrincipal = (ClaimsPrincipal)principal;
            return claimsPrincipal.GetUserId();
        }
    }
}
