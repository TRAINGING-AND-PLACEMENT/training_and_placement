using Demo.api;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Demo.Controllers.Json;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Demo.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        JsonView cj =new JsonView();
        public CompanyController(IHttpContextAccessor httpContextAccessor)
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = httpContextAccessor;
        }
        public IActionResult Index()
        {   
            if(@context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<Company> model = new List<Company>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    model = cj.listroot(model, data);
                    Debug.WriteLine(model);
                }
                return View(model);
            }
            else
            {
                TempData["error"] = "You have to login with co-ordinator id and password to access the page.";
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult Create()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                return View();
            }
            else
            {
                TempData["error"] = "You have to login with co-ordinator id and password to access the page.";
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "companydetails", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        Debug.Write(result);
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            else
            {
                TempData["error"] = "You have to login with co-ordinator id and password to access the page.";
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult Edit_Company(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2){
                Company model = new Company();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getcompanydetails&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    model = cj.uniroot(model, data);
                }
                return View(model);
            }
            else
            {
                TempData["error"] = "You have to login with co-ordinator id and password to access the page.";
                return RedirectToAction("Login", "User");
            }
            
        }
        [HttpPost]
        public IActionResult Update_Company(Company model) 
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "updatecompanydetails&id=" + model.id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("Index");
                }
                return View();
            }
            else
            {
                TempData["error"] = "You have to login with co-ordinator id and password to access the page.";
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult Delete_Company(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "deletecompanydetails&id=" +id).Result;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "You have to login with co-ordinator id and password to access the page.";
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult AllCompanies()
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

        public IActionResult AppliedCompanies()
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
        public IActionResult SelectedCompanies()
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
        public IActionResult CompanyDetails()
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
        public IActionResult AddCompany()
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
    }
}
