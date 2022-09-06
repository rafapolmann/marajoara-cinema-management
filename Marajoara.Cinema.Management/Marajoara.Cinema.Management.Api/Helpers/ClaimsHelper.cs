using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Marajoara.Cinema.Management.Api.Helpers
{
    public static class ClaimsHelper
    {
        public static int GetUserAccountID(IHttpContextAccessor context)
        {
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity.FindFirst("UserAccountID")?.Value);
        }
    }
}
