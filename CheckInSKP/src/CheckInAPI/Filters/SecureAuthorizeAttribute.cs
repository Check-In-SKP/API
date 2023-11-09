using Microsoft.AspNetCore.Mvc;

namespace CheckInAPI.Filters
{
    public class SecureAuthorizeAttribute : TypeFilterAttribute
    {
        public SecureAuthorizeAttribute() : base(typeof(SecureAuthorizeFilter))
        {
        }
    }
}
