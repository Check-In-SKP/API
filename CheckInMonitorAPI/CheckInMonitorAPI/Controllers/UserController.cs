using Microsoft.AspNetCore.Mvc;

namespace CheckInMonitorAPI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
