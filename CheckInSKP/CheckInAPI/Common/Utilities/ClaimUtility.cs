using System.Security.Claims;

namespace CheckInAPI.Common.Utilities
{
    public class ClaimUtility
    {
        public static(int? userId, int? roleId) ParseUserAndRoleClaims(ClaimsPrincipal user)
        {
            int? userId = null;
            int? roleId = null;

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            var userRoleClaim = user.FindFirst(ClaimTypes.Role);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int uid))
                userId = uid;

            if (userRoleClaim != null && int.TryParse(userRoleClaim.Value, out int rid))
                roleId = rid;

            return (userId, roleId);
        }
    }
}
