using Microsoft.AspNetCore.Mvc;

namespace Demo.Models
{
    public class Hiring : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
