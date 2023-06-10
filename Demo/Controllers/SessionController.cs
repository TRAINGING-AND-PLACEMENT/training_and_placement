using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Demo.Controllers
{
    public class SessionController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public SessionController(IHttpContextAccessor httpContextAccessor)
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
        public IActionResult SessionView()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<Session_insert> model = new List<Session_insert>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getsessiondetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var sessions = JsonDecode.FromJson(data);
                    foreach (var session in sessions.validsession)
                    {
                        model.Add(session);
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
        public IActionResult SessionInsert(){ return View(); }
        [HttpPost]
        public IActionResult SessionInsert(Session_insert session) 
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(session);
                    Debug.WriteLine(data);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "insertsession", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        var msg = JsonDecode.FromJson(result);
                        if (msg.Success)
                        {
                            TempData["success"] = msg.message;
                        }
                        return RedirectToAction("SessionView");
                    }
                }
                return View(session);
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
            return View(); 
        }
        public IActionResult SessionEdit(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                Session_insert model = new Session_insert();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getsessiondetails&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var session = JsonDecode.FromJson(data);
                    model = session.validsession[0];
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
        public IActionResult SessionUpdate(Session_insert session)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(session);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "updatesessiondetails&id=" + session.id, content).Result;
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
                        return RedirectToAction("SessionView");
                    }
                    return View();
                }
                return View(session);
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult SessionDelete(int id) 
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "deletesessiondetails&id=" + id).Result;
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
                return RedirectToAction("SessionView");
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult SessionDefault(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "setdefaultsession&id=" + id).Result;
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
                return RedirectToAction("SessionView");
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult StudentSession()
        {
            List<Sessions> sessions = new List<Sessions>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getsessiondetails").Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(data);
                var session = JsonDecode.FromJson(data);
                foreach (var ss in session.session)
                {
                    sessions.Add(new Sessions
                    {
                        id = ss.id,
                        label = ss.label,
                        default_year = ss.default_year
                    });
                }
            }

            List<Department> departments = new List<Department>();
            HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "get_department").Result;
            if (response1.IsSuccessStatusCode)
            {
                String data = response1.Content.ReadAsStringAsync().Result;
                var department = JsonDecode.FromJson(data);
                foreach (var dept in department.departments)
                {
                    departments.Add(new Department
                    {
                        id = dept.id,
                        department = dept.department
                    });
                }
            }
            List<Student> students = new List<Student>();
            HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "getstudentdetail").Result;
            if (response2.IsSuccessStatusCode)
            {
                String data = response2.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(data);
                var std = JsonDecode.FromJson(data);
                if (std.Success)
                {
                    foreach (var student in std.students)   
                    {
                        students.Add(new Student 
                        {
                            id = student.id, department_id=student.department_id, session_id=student.session_id, 
                            surname = student.surname, first_name = student.first_name, last_name = student.last_name,
                            enrollment = student.enrollment, gender= student.gender, contact = student.contact,
                            address = student.address, pincode = student.pincode, city = student.city, state = student.state
                        });
                    }
                }
            }

            ViewBag.sessions = sessions;
            ViewBag.department = departments;
            ViewBag.students = students;
            return View();
        }
        public IActionResult StudentDeafultSession(int id, IFormCollection collection)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                var session = collection["session"];
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "updatestudentsession&id=" + id +"&sid=" + session).Result;
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
                return RedirectToAction("StudentSession");
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
            return View();
        }
    }
}
