using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.Controllers
{
    public class HiringController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public HiringController(IHttpContextAccessor httpContextAccessor)
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = httpContextAccessor;
        }
        public void DestorySession()
        {
            context.HttpContext.Session.Remove("role");
            context.HttpContext.Session.Remove("userid");
            context.HttpContext.Session.Remove("sessionid");
            context.HttpContext.Session.Remove("studentid");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HiringCompanies() {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                List<Hiring> model = new List<Hiring>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "gethiringdetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var res = JsonDecode.FromJson(data);
                    foreach (var hiring in res.hirings)
                    {
                        model.Add(hiring);
                    }
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult Create() 
        {

            return View(); 
        }    
    }
}
