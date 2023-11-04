using CheckInAPI.Common.Utilities;
using CheckInSKP.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CheckInAPI.Filters
{
    public class SecureAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IRoleValidationService _roleValidationService;

        public SecureAuthorizeFilter(IRoleValidationService roleValidationService)
        {
            _roleValidationService = roleValidationService;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var (userId, userRoleId) = ClaimUtility.ParseUserAndRoleClaims(context.HttpContext.User);

            if (!userId.HasValue || !userRoleId.HasValue)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!await _roleValidationService.UserHasValidRole(userId.Value, userRoleId.Value))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
