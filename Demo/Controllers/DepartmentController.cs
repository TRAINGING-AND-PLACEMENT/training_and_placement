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
    public class DepartmentController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public DepartmentController(IHttpContextAccessor httpContextAccessor)
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
        public IActionResult UploadDepartment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadDepartment(IFormFile CSV_File, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            string filename = $"{webHostEnvironment.WebRootPath}\\files\\user_csv\\{CSV_File.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                CSV_File.CopyTo(fileStream);
            }

            List<Department> model = new List<Department>();
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
                    return RedirectToAction("View_Department");
                }
            }
            //TempData["error"] = "choose any role to sign in.";
            return RedirectToAction("Department");
        }

        public IActionResult Insert_dept()
        {
            return View();
        }

        //single department insert
        [HttpPost]
        public IActionResult Insert_dept(Department model)
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
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_department", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        Debug.Write(result);
                        TempData["success"] = "Department inserted.";
                        return RedirectToAction("View_Department");
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

        public IActionResult View_Department()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<Department> model = new List<Department>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_department").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var Departments = JsonDecode.FromJson(data);
                    foreach (var department in Departments.departments)
                    {
                        model.Add(department);
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
        public IActionResult Edit_Department(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                Department model = new Department();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_department&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var department = JsonDecode.FromJson(data);
                    Debug.WriteLine(department);
                    model = department.departments[0];
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
        public IActionResult Update_Department(Department model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "update_department&id=" + model.id, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        TempData["success"] = "Department Updated";
                        return RedirectToAction("View_Department");
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



        public IActionResult Delete_Department(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "delete_department&id=" + id).Result;
                return RedirectToAction("View_Department");
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
