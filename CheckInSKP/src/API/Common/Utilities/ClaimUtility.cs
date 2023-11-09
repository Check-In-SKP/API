using System.Security.Claims;

namespace API.Common.Utilities
{
    public class ClaimUtility
    {
        public static (int? userId, int? roleId) ParseUserAndRoleClaims(ClaimsPrincipal user)
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

        public static (Guid? deviceId, int? userId, string? username, int? roleId) ParseTokenClaims(ClaimsPrincipal token)
        {
            Guid? deviceId = null;
            int? userId = null;
            string? username = null;
            int? roleId = null;

            var deviceIdClaim = token.FindFirst(ClaimTypes.SerialNumber);
            var userIdClaim = token.FindFirst(ClaimTypes.NameIdentifier);
            var usernameClaim = token.FindFirst(ClaimTypes.Name);
            var userRoleClaim = token.FindFirst(ClaimTypes.Role);

            if (deviceIdClaim != null && Guid.TryParse(deviceIdClaim.Value, out Guid did))
                deviceId = did;

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int uid))
                userId = uid;

            if (usernameClaim != null)
                username = usernameClaim.Value;

            if (userRoleClaim != null && int.TryParse(userRoleClaim.Value, out int rid))
                roleId = rid;

            return (deviceId, userId, username, roleId);
        }
    }
}
