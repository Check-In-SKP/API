using CheckInSKP.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CheckInAPI.Filters
{
    public class AuthorizaUserRoleFilter : IAuthorizationFilter
    {
        private readonly IRoleValidationService _roleValidationService;

        public AuthorizaUserRoleFilter(IRoleValidationService roleValidationService)
        {
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
            if(roleIdClaims == null || !int.TryParse(roleIdClaims.Value, out var roleId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if(!_roleValidationService.UserHasValidRole(userId, roleId).Result)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
