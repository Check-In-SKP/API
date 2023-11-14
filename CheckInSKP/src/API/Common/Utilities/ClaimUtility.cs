using System.Security.Claims;

namespace API.Common.Utilities
{
    public class ClaimUtility
    {
        public static (Guid? userId, int? roleId) ParseUserAndRoleClaims(ClaimsPrincipal user)
        {
            Guid? userId = null;
            int? roleId = null;

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            var userRoleClaim = user.FindFirst(ClaimTypes.Role);

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid uid))
                userId = uid;

            if (userRoleClaim != null && int.TryParse(userRoleClaim.Value, out int rid))
                roleId = rid;

            return (userId, roleId);
        }

        public static (Guid? deviceId, Guid? userId, string? username, int? roleId) ParseTokenClaims(ClaimsPrincipal token)
        {
            Guid? deviceId = null;
            Guid? userId = null;
            string? username = null;
            int? roleId = null;

            var deviceIdClaim = token.FindFirst(ClaimTypes.SerialNumber);
            var userIdClaim = token.FindFirst(ClaimTypes.NameIdentifier);
            var usernameClaim = token.FindFirst(ClaimTypes.Name);
            var userRoleClaim = token.FindFirst(ClaimTypes.Role);

            if (deviceIdClaim != null && Guid.TryParse(deviceIdClaim.Value, out Guid did))
                deviceId = did;

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid uid))
                userId = uid;

            if (usernameClaim != null)
                username = usernameClaim.Value;

            if (userRoleClaim != null && int.TryParse(userRoleClaim.Value, out int rid))
                roleId = rid;

            return (deviceId, userId, username, roleId);
        }
    }
}
