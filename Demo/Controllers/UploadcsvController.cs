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
        public void DestorySession()
        {
            context.HttpContext.Session.Remove("role");
            context.HttpContext.Session.Remove("userid");
            context.HttpContext.Session.Remove("sessionid");
            context.HttpContext.Session.Remove("studentid");
        }
        public IActionResult Department()
        {
            return View("~/Views/Uploadcsv/Department.cshtml");
        }
        [HttpPost]
        public IActionResult Department(IFormFile CSV_File, [FromServices] IWebHostEnvironment webHostEnvironment)
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
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_sector", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        Debug.Write(result);
                        TempData["success"] = "Sector inserted.";
                        return RedirectToAction("View_Sector");
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

        public IActionResult Sector()
        {
            return View("~/Views/Uploadcsv/Sector.cshtml");
        }
        [HttpPost]
        public IActionResult Sector(IFormFile CSV_File, [FromServices] IWebHostEnvironment webHostEnvironment)
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
                    TempData["success"] = "Sector inserted.";
                    return RedirectToAction("View_Sector");
                }
            }

            return RedirectToAction("Sector");
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
                return RedirectToAction("View_Sector");
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        //user details
        public IActionResult Index()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                var departmentmodel = new List<Department>();
                List<User> usermodel = new List<User>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_student_user").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var users = JsonDecode.FromJson(data);
                    foreach (var user in users.user)
                    {
                        usermodel.Add(user);
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
                UploadCsv dv = new UploadCsv();
                dv.Departments = departmentmodel;
                ViewBag.StudentUsers = usermodel;
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Import(UploadCsv dp, IFormFile CSV_File, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            string filename = $"{webHostEnvironment.WebRootPath}\\files\\user_csv\\{CSV_File.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filename))
            {
                CSV_File.CopyTo(fileStream);
            }

                List<User> model = new List<User>();

            var did = dp.department_id;

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
                    var user = csv.GetRecord<User>();
                    user.role = 1;
                    user.status = "0";
                    user.created_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    user.updated_at = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                    model.Add(user);
                }
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_user&did="+did, content).Result;
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
        public IActionResult insert_user()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                
                var departmentmodel = new List<Department>();
                var sessionmodel = new List<Sessions>();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_department").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
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

                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "getsession").Result;
                if (response1.IsSuccessStatusCode)
                {
                    String data = response1.Content.ReadAsStringAsync().Result;
                    var session = JsonDecode.FromJson(data);
                    foreach (var sessions in session.session)
                    {
                        var Session = new Sessions
                        {
                            id = sessions.id,
                            label = sessions.label,
                        };
                        sessionmodel.Add(Session);
                    }
                }

                Insert_User insert_user = new Insert_User();
                insert_user.Departments = departmentmodel;
                insert_user.Sessions = sessionmodel;
                return View(insert_user);
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
        public IActionResult insert_user(Insert_User model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    Debug.WriteLine(data);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "insert_user", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        Debug.Write(result);
                        TempData["success"] = "User inserted";
                        return RedirectToAction("Index", "Home");
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

    }
}
