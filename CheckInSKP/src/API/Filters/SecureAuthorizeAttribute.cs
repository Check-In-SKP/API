using Microsoft.AspNetCore.Mvc;

namespace API.Filters
{
    public class SecureAuthorizeAttribute : TypeFilterAttribute
    {
        public SecureAuthorizeAttribute() : base(typeof(SecureAuthorizeFilter))
        {
        }
    }
}
