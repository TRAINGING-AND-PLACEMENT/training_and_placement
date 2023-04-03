using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Demo.api;
using Demo.Models;
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
        HttpClient client;
        public UploadcsvController()
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
                    return RedirectToAction("Index");
                }
            }
            

            return Index();
        }
    }
}
