using CheckInAPI.Common.Utilities;
using CheckInSKP.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CheckInAPI.Filters
{
    /// <summary>
    /// Class <c>SecureAuthorizeFilter</c> filters out invalid tokens or tokens which no longer represents data in a valid state.
    /// 
    /// Cautions:
    ///     Performance heavy / slow.
    ///     Only use this filter on endpoints which require a valid token.
    /// </summary>
    public class SecureAuthorizeFilter : IAuthorizationFilter
    {
        private readonly ITokenValidationService _tokenValidationService;

        public SecureAuthorizeFilter(ITokenValidationService tokenValidationService)
        {
            _tokenValidationService = tokenValidationService;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var (deviceId, userId, username, roleId) = ClaimUtility.ParseTokenClaims(context.HttpContext.User);

            if (deviceId == null || userId == null || username == null || roleId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Checks the database if the token claims are still valid.
            if (!await _tokenValidationService.ValidateUserClaims((int)userId, username, (int)roleId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Checks the database if the device is still authorized.
            if (!await _tokenValidationService.DeviceIsAuthorized((Guid)deviceId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
