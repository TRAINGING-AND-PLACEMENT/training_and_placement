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
        HttpClient client;
        CompanyJson cj =new CompanyJson();
        public CompanyController()
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            /*List<Company> model = new List<Company>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_user").Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                model = cj.listroot(model, data);
            }*/
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company model)
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

        public IActionResult AllCompanies()
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
        public IActionResult AddCompany()
        {
            return View();
        }
    }
}
