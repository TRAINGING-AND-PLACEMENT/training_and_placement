using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class HiringController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
