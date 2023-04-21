using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Demo.api;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.CodeAnalysis.Elfie.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Demo.Controllers
{
    public class UploadcsvController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public UploadcsvController(IHttpContextAccessor httpContextAccessor)
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = httpContextAccessor;
        }
        public IActionResult Department()
        {
            return View("~/Views/Uploadcsv/Department.cshtml");
        }
        [HttpPost]
        public IActionResult Department(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            string filename = $"{webHostEnvironment.WebRootPath}\\files\\user_csv\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                file.CopyTo(fileStream);
            }

            List<Department> model = new List<Department>();
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\user_csv"}" + "\\" + file.FileName;

            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8,
                MissingFieldFound = null
            };
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var department = csv.GetRecord<Department>();
                    department.status = 0;
                    department.remarks = "";
                    department.created_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    department.updated_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    model.Add(department);
                }
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_department", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data2 = response.Content.ReadAsStringAsync().Result;
                    Debug.Write(data2);
                    TempData["success"] = "Department inserted.";
                    return RedirectToAction("Department");
                }
            }
            //TempData["error"] = "choose any role to sign in.";
            return RedirectToAction("Department");
        }

        public IActionResult Sector()
        {
            return View("~/Views/Uploadcsv/Sector.cshtml");
        }
        [HttpPost]
        public IActionResult Sector(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            string filename = $"{webHostEnvironment.WebRootPath}\\files\\user_csv\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                file.CopyTo(fileStream);
            }

            List<Sector> model = new List<Sector>();
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\user_csv"}" + "\\" + file.FileName;

            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8,
                MissingFieldFound = null
            };
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var sector = csv.GetRecord<Sector>();
                    sector.status = 0;
                    sector.remarks = "";
                    sector.created_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    sector.updated_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    model.Add(sector);
                }
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_sector", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data2 = response.Content.ReadAsStringAsync().Result;
                    Debug.Write(data2);
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Sector");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Import(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            string filename = $"{webHostEnvironment.WebRootPath}\\files\\user_csv\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                file.CopyTo(fileStream);
            }

            List<User> model = new List<User>();
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\user_csv"}" + "\\" + file.FileName;

            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8,
                MissingFieldFound = null
            };
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var user = csv.GetRecord<User>();
                    user.role = 1;
                    user.status = "0";
                    user.created_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    user.updated_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    model.Add(user);
                }
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_user", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data2 = response.Content.ReadAsStringAsync().Result;
                    Debug.Write(data2);
                    TempData["success"] = "User inserted.";
                    return RedirectToAction("Index");
                }
            }
            

            return Index();
        }
    }
}
