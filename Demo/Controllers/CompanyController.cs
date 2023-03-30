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
    }
}
