using Microsoft.AspNetCore.Mvc;

namespace TnP.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AppliedCompanies()
        {
            return View();
        }
        public IActionResult SelectedCompanies()
        {
            return View();
        }
        public IActionResult CompanyDetails()
        {
            return View();
        }
    }
}
