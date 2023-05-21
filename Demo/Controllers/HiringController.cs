using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using System.Diagnostics;
using System.Text;

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

        public IActionResult CreateHiring() {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                var companymodel = new List<Company>();
                var departmentmodel = new List<Department>();
                var sectormodel = new List<Sector>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;                    
                    var companies = JsonDecode.FromJson(data);
                    foreach (var company in companies.Companies)
                    {
                        var Company = new Company
                        {
                            id = company.id,
                            name = company.name
                        };
                        companymodel.Add(Company);
                    }
                }
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "get_department").Result;
                if (response2.IsSuccessStatusCode)
                {
                    String data = response2.Content.ReadAsStringAsync().Result;
                    var department = JsonDecode.FromJson(data);
                    foreach (var departments in department.departments)
                    {
                        var Department = new Department
                        {
                            id = departments.id,
                            department = departments.department
                        };
                        departmentmodel.Add(Department);
                    }
                }
                HttpResponseMessage response3 = client.GetAsync(client.BaseAddress + "get_sector").Result;
                if (response3.IsSuccessStatusCode)
                {
                    String data = response3.Content.ReadAsStringAsync().Result;
                    var sector = JsonDecode.FromJson(data);
                    foreach (var sectors in sector.sectors)
                    {
                        var Sector = new Sector
                        {
                           id= sectors.id,
                           sector = sectors.sector
                        };
                        sectormodel.Add(Sector);
                    }
                }
                ViewCompnaySession viewCompnaySession = new ViewCompnaySession();   
                viewCompnaySession.Companies = companymodel;
                viewCompnaySession.Departments = departmentmodel;
                viewCompnaySession.Sectors = sectormodel;
                return View(viewCompnaySession);
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateHiring(ViewCompnaySession model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    Debug.WriteLine(content);
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "addhiringdetails", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        Debug.Write(result);
                        TempData["success"] = "Hiring successfully added.";
                        return RedirectToAction("CreateHiring");
                    }
                }

                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult HiringCompanies() {
            if (@context.HttpContext.Session.GetInt32("role") == 1 || @context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<Hiring> hiringmodel = new List<Hiring>();
                var companymodel = new List<Company>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "gethiringdetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var res = JsonDecode.FromJson(data);
                    foreach (var hiring in res.hirings)
                    {
                        hiringmodel.Add(hiring);
                    }
                }
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response2.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);
                    foreach (var company in companies.Companies)
                    {
                        var Company = new Company
                        {
                            id = company.id,
                            name = company.name
                        };
                        companymodel.Add(Company);
                    }
                }
                ViewHiring viewHiring   = new ViewHiring();
                viewHiring.Companies = companymodel;
                viewHiring.Hirings = hiringmodel;
                return View(viewHiring);
            }
            else
            {
                TempData["serror"] = "You have to login to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        
        public IActionResult EditHiring(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1 || @context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<Hiring> hiringmodel = new List<Hiring>();
                var companymodel = new List<Company>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "gethiringdetails&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var res = JsonDecode.FromJson(data);
                    foreach (var hiring in res.hirings)
                    {
                        hiringmodel.Add(hiring);
                    }
                }
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response2.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);
                    foreach (var company in companies.Companies)
                    {
                        var Company = new Company
                        {
                            id = company.id,
                            name = company.name
                        };
                        companymodel.Add(Company);
                    }
                }
                ViewHiring viewHiring = new ViewHiring();
                viewHiring.Companies = companymodel;
                viewHiring.Hirings = hiringmodel;
                return View(viewHiring);
            }
            else
            {
                TempData["serror"] = "You have to login to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult DeleteHiring(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "deletehiringdetails&id=" + id).Result;
                TempData["success"] = "Hiring successfully deleted.";
                return RedirectToAction("HiringCompanies");
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
    }
}
