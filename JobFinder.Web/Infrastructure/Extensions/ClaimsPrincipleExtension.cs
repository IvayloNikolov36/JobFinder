using System.Security.Claims;

namespace JobFinder.Web.Infrastructure.Extensions
{
    public static class ClaimsPrincipleExtension
    {
        public static string GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
