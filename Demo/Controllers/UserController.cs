using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
