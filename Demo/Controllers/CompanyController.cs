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
        CompanyJson cj = new CompanyJson();
        public CompanyController()
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
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
        public IActionResult Edit_Company(int id)
        {
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
        [HttpPost]
        public IActionResult Update_Company(Company model) {

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

        public IActionResult Delete_Company(int id)
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "deletecompanydetails&id=" +id).Result;
            return RedirectToAction("Index");
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
