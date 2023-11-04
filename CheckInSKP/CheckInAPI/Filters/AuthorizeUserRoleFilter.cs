using CheckInSKP.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CheckInAPI.Filters
{
    public class AuthorizeUserRoleFilter : IAuthorizationFilter
    {
        private readonly int[] _roleIds;
        private readonly IRoleValidationService _roleValidationService;

        public AuthorizeUserRoleFilter(int[] roleIds, IRoleValidationService roleValidationService)
        {
            _roleIds = roleIds;
            _roleValidationService = roleValidationService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdClaims = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var roleIdClaims = context.HttpContext.User.FindFirst(ClaimTypes.Role);

            if(userIdClaims == null || !int.TryParse(userIdClaims.Value, out var userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if(!_roleValidationService.UserHasValidRole(userId, _roleIds).Result)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
