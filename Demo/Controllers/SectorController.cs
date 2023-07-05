using Azure;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Microsoft.CodeAnalysis.Elfie.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace Demo.Controllers
{
    public class SectorController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public SectorController(IHttpContextAccessor httpContextAccessor)
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
        [HttpGet]
        public IActionResult validSector(String sec)
        {
            bool isValid = true;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "valid_sector&sector=" + sec).Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                var msg = JsonDecode.FromJson(data);
                if (msg.Success)
                {
                    isValid = msg.Success;
                }
                else
                {
                    isValid = msg.Success;
                }
            }

            return Json(new { isValid });
        }
        public IActionResult UploadSector()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadSector(IFormFile CSV_File, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            string filename = $"{webHostEnvironment.WebRootPath}\\files\\user_csv\\{CSV_File.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                CSV_File.CopyTo(fileStream);
            }

            List<Sector> model = new List<Sector>();
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\user_csv"}" + "\\" + CSV_File.FileName;

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
                    var msg = JsonDecode.FromJson(data2);
                    if (msg.Success)
                    {
                        TempData["success"] = msg.message + " and " + msg.datacount + " repeated data skipped.";
                    }
                    else
                    {
                        TempData["error"] = msg.message;
                    }
                    return RedirectToAction("View_Sector");
                }
            }

            return RedirectToAction("Sector");
        }
        public IActionResult Insert_sect()
        {
            return View();
        }

        //single sector insert
        [HttpPost]
        public IActionResult Insert_sect(Sector model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    model.status = 0;
                    model.remarks = "";
                    model.created_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    model.updated_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    String data = JsonConvert.SerializeObject(model);
                    Debug.WriteLine(data);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_single_sector", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        var msg = JsonDecode.FromJson(result);
                        if (msg.Success)
                        {
                            TempData["success"] = msg.message;
                        }
                        else
                        {
                            TempData["error"] = msg.message;
                        }
                        return View();
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

        
        public IActionResult Edit_Sector(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                Sector model = new Sector();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_sector&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var sector = JsonDecode.FromJson(data);
                    Debug.WriteLine(sector);
                    model = sector.sectors[0];
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
        public IActionResult Update_Sector(Sector model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "update_sector&id=" + model.id, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        TempData["success"] = "Sector Updated.";
                        return RedirectToAction("View_Sector");
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
        public IActionResult View_Sector()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<Sector> model = new List<Sector>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_sector").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var Sectors = JsonDecode.FromJson(data);
                    foreach (var sector in Sectors.sectors)
                    {
                        model.Add(sector);
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
            return View();
        }

        public IActionResult Delete_Sector(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "delete_sector&id=" + id).Result;
                TempData["success"] = "Sector Deleted.";
                return RedirectToAction("View_Sector");
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
