using Demo.api;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Demo.Controllers.Json;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Data;


namespace Demo.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public CompanyController(IHttpContextAccessor httpContextAccessor)
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
            if(@context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<Company> model = new List<Company>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);
                    foreach(var company in companies.Companies)
                    {
                        model.Add(company);
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
        public IActionResult Create()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                return View();
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
        public IActionResult Create(Company model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    Debug.WriteLine(data);
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
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
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
                    var company = JsonDecode.FromJson(data);
                    Debug.WriteLine(company);
                    model = company.Company[0];
                    Debug.WriteLine(model);
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
        [HttpPost]
        public IActionResult Update_Company(Company model) 
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
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
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
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
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult AllCompanies()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                List<Company> model = new List<Company>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);
                    foreach(var company in companies.Companies)
                    {
                        model.Add(company);
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

        public IActionResult AppliedCompanies()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
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
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult CompanyDetails(string ids)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Debug.WriteLine(ids);
                string[] idArray = ids.Split(',');
                int cid = 0;
                int hid = 0;
                for (int i = 0; i < idArray.Length; i++)
                { 
                    cid = int.Parse(idArray[0]);
                    hid = int.Parse(idArray[1]);
                }
                
                Debug.WriteLine(cid);
                Debug.WriteLine(hid);
                var hiringmodel = new List<Hiring>();
                var companymodel = new List<Company>();
                
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getcompanydetails&id="+cid).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);
                    foreach (var company in companies.Company)
                    { 
                        companymodel.Add(company);
                    }
                    Debug.WriteLine(companymodel);
                }

                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "gethiringdetails&id="+hid).Result;
                if (response1.IsSuccessStatusCode)
                {
                    String data = response1.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var hirings = JsonDecode.FromJson(data);
                    foreach (var hiring in hirings.Hiring)
                    {
                        hiringmodel.Add(hiring);
                    }
                }
                ViewHiring viewStudentHiring = new ViewHiring();
                viewStudentHiring.Companies = companymodel;
                viewStudentHiring.Hirings = hiringmodel;
                return View(viewStudentHiring);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
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
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
    }
}
