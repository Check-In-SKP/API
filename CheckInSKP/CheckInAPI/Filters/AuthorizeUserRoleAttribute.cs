using Microsoft.AspNetCore.Mvc;

namespace CheckInAPI.Filters
{
    public class AuthorizeUserRoleAttribute : TypeFilterAttribute
    {
        public AuthorizeUserRoleAttribute(params int[] roleIds) : base(typeof(AuthorizeUserRoleFilter))
        {
            Arguments = new object[] { roleIds };
        }
    }
}
