using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentProfile() {
            return View();
        }
    }
}
