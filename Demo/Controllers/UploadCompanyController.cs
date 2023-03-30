using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class UploadCompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
