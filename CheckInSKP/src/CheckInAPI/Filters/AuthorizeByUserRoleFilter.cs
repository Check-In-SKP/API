using CheckInAPI.Common.Utilities;
using CheckInSKP.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CheckInAPI.Filters
{
    public class AuthorizeByUserRoleFilter : IAuthorizationFilter
    {
        private readonly int[] _roleIds;
        private readonly ITokenValidationService _roleValidationService;

        public AuthorizeByUserRoleFilter(int[] roleIds, ITokenValidationService roleValidationService)
        {
            _roleIds = roleIds;
            _roleValidationService = roleValidationService;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var (userId, _) = ClaimUtility.ParseUserAndRoleClaims(context.HttpContext.User);

            if (!userId.HasValue)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!await _roleValidationService.UserHasValidRole(userId.Value, _roleIds))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
