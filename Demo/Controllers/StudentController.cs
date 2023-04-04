using Demo.api;
using Demo.Controllers.Json;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public StudentController(IHttpContextAccessor httpContextAccessor)
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = httpContextAccessor;
        }
        public IActionResult Index()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                return View();
            }
            else
            {
                TempData["error"] = "You have to login with student id and password to access the page.";
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult StudentProfile() {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                return View();
            }
            else
            {
                TempData["error"] = "You have to login with student id and password to access the page.";
                return RedirectToAction("Login", "User");
            }
        }
    }
}
