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
            
        }
        public IActionResult ImportUser()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                var departmentmodel = new List<Department>();
                var sessionmodel = new List<Sessions>();

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
                HttpResponseMessage sessionresponse = client.GetAsync(client.BaseAddress + "getsessiondetails").Result;
                if (sessionresponse.IsSuccessStatusCode)
                {
                    String data = sessionresponse.Content.ReadAsStringAsync().Result;
                    var sessions = JsonDecode.FromJson(data);
                    foreach (var session in sessions.session)
                    {
                        sessionmodel.Add(session);
                    }
                }
                UploadCsv dv = new UploadCsv();
                dv.Departments = departmentmodel;
                dv.Sessions = sessionmodel;
                return View(dv);
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
            var sid = dp.session_id;

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
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "set_user&did="+did+"&sid="+sid, content).Result;
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

                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "getsessiondetails").Result;
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

        public IActionResult EditUser(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                var departmentmodel = new List<Department>();
                var sessionmodel = new List<Sessions>();
                var insert_user = new Insert_User();

                HttpResponseMessage userresponse = client.GetAsync(client.BaseAddress + "getuserdetails&id="+id).Result;
                if (userresponse.IsSuccessStatusCode)
                {
                    String data = userresponse.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var users = JsonDecode.FromJson(data);
                    insert_user = users.getuser[0];
                }
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

                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "getsessiondetails").Result;
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
            return View();
        }
        public IActionResult UpdateUser(Insert_User model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "updateuserdetails&id=" + model.id, content).Result;
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
        public IActionResult DeleteUser(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "deleteuserdetails&id=" + id).Result;
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
                return RedirectToAction("Index");
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
