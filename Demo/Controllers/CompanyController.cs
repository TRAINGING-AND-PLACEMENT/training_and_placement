using Demo.api;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class CompanyController : Controller
    {
        HttpClient client;
        public CompanyController() 
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
