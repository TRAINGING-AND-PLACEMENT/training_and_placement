using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Demo.Controllers
{

    public class HiringController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public HiringController(IHttpContextAccessor httpContextAccessor)
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateHiring()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                var companymodel = new List<Company>();
                var departmentmodel = new List<Department>();
                var sectormodel = new List<Sector>();
                var sessionmodel = new List<Sessions>();
                


                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var companies = JsonDecode.FromJson(data);
                    foreach (var company in companies.Companies)
                    {
                        var Company = new Company
                        {
                            id = company.id,
                            name = company.name
                        };
                        companymodel.Add(Company);
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
                HttpResponseMessage response3 = client.GetAsync(client.BaseAddress + "get_sector").Result;
                if (response3.IsSuccessStatusCode)
                {
                    String data = response3.Content.ReadAsStringAsync().Result;
                    var sector = JsonDecode.FromJson(data);
                    foreach (var sectors in sector.sectors)
                    {
                        var Sector = new Sector
                        {
                            id = sectors.id,
                            sector = sectors.sector
                        };
                        sectormodel.Add(Sector);
                    }
                }
                HttpResponseMessage response4 = client.GetAsync(client.BaseAddress + "getsessiondetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response4.Content.ReadAsStringAsync().Result;
                    var sessions = JsonDecode.FromJson(data);
                    foreach (var session in sessions.session)
                    {
                        sessionmodel.Add(session);
                    }
                }
                ViewCompanySession viewCompanaySession = new ViewCompanySession();
                viewCompanaySession.Companies = companymodel;
                viewCompanaySession.Departments = departmentmodel;
                viewCompanaySession.Sectors = sectormodel;
                viewCompanaySession.Sessions = sessionmodel;
                return View(viewCompanaySession);
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
        public IActionResult CreateHiring(ViewCompanySession model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                Debug.WriteLine(model);
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    Debug.WriteLine(data);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "addhiringdetails", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        Debug.Write(result);
                        TempData["success"] = "Hiring successfully added.";

                    }
                    return RedirectToAction("CordHiringCompanies");
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
        public IActionResult HiringCompanies()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                var hiringmodel = new List<Hiring>();
                var companymodel = new List<Company>();
                var sessionmodel = new List<Sessions>();
                var studentApplicationModel = new List<StudentApplication>();
                var studentModel = new List<Student>();
                var hiringDepartmentModel = new List<Hiring_Departments>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "gethiringdetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var res = JsonDecode.FromJson(data);
                    if (res.Success)
                    {
                        foreach (var hiring in res.hirings)
                        {
                            hiringmodel.Add(hiring);
                        }
                    }
                }
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response2.IsSuccessStatusCode)
                {
                    String data = response2.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);
                    foreach (var company in companies.Companies)
                    {
                        var Company = new Company
                        {
                            id = company.id,
                            name = company.name
                        };
                        companymodel.Add(Company);
                    }
                }
                HttpResponseMessage response3 = client.GetAsync(client.BaseAddress + "getsessiondetails").Result;
                if (response3.IsSuccessStatusCode)
                {
                    String data = response3.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var sessions = JsonDecode.FromJson(data);
                    foreach (var session in sessions.session)
                    {
                        var Session = new Sessions
                        {
                            id = session.id,
                            label = session.label
                        };
                        sessionmodel.Add(Session);
                    }
                }
                HttpResponseMessage response4 = client.GetAsync(client.BaseAddress + "get_student_application").Result;
                if (response4.IsSuccessStatusCode)
                {
                    String data = response4.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var student_applications = JsonDecode.FromJson(data);
                    if (student_applications.Success)
                    {
                        foreach (var student in student_applications.applications)
                        {
                            var Student = new StudentApplication
                            {
                                id = student.id,
                                student_id = student.student_id,
                                hiring_id = student.hiring_id,
                                min_stipend = student.min_stipend,
                                max_stipend = student.max_stipend,
                                min_salary = student.min_salary,
                                max_salary = student.max_salary,
                                date_of_joining = student.date_of_joining,
                                status = student.status,
                                created_at = student.created_at,
                                updated_at = student.updated_at

                            };
                            studentApplicationModel.Add(Student);
                        }
                    }
                }
                HttpResponseMessage response5 = client.GetAsync(client.BaseAddress + "getstudentdetail&id="+ @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response5.IsSuccessStatusCode)
                {
                    String data = response5.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var Students = JsonDecode.FromJson(data);
                    if (Students.Success)
                    {
                        foreach (var student in Students.studentInfo)
                        {
                            var Student = new Student
                            {
                                department_id = student.department_id
                            };
                            studentModel.Add(Student);
                        }
                    }
                }
                HttpResponseMessage response6 = client.GetAsync(client.BaseAddress + "gethiringdepartments").Result;
                if (response5.IsSuccessStatusCode)
                {
                    String data = response6.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var hDepart = JsonDecode.FromJson(data);
                    if (hDepart.Success)
                    {
                        foreach (var departments in hDepart.Hiring_Departments)
                        {
                            var hdepart = new Hiring_Departments
                            {
                                hiring_id = departments.hiring_id,
                                department_id = departments.department_id

                            };
                            hiringDepartmentModel.Add(hdepart);
                        }
                    }
                }

                ViewHiring viewHiring = new ViewHiring();
                viewHiring.Companies = companymodel;
                viewHiring.Hirings = hiringmodel;
                viewHiring.Session = sessionmodel;
                viewHiring.applications = studentApplicationModel;
                viewHiring.students = studentModel;
                viewHiring.hiring_Departments = hiringDepartmentModel;

                return View(viewHiring);
            }
            else
            {
                TempData["serror"] = "You have to login to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }


        public IActionResult CordHiringCompanies()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                var hiringmodel = new List<Hiring>();
                var companymodel = new List<Company>();
                var sessionmodel = new List<Sessions>();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "gethiringdetails").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var res = JsonDecode.FromJson(data);
                    if (res.Success)
                    {
                        foreach (var hiring in res.hirings)
                        {
                            hiringmodel.Add(hiring);
                        }
                    }
                }
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response2.IsSuccessStatusCode)
                {
                    String data = response2.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);
                    foreach (var company in companies.Companies)
                    {
                        var Company = new Company
                        {
                            id = company.id,
                            name = company.name
                        };
                        companymodel.Add(Company);
                    }
                }
                HttpResponseMessage response3 = client.GetAsync(client.BaseAddress + "getsessiondetails").Result;
                if (response3.IsSuccessStatusCode)
                {
                    String data = response3.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var sessions = JsonDecode.FromJson(data);
                    foreach (var session in sessions.session)
                    {
                        var Session = new Sessions
                        {
                            id = session.id,
                            label = session.label
                        };
                        sessionmodel.Add(Session);
                    }
                }
               
                ViewHiring viewHiring = new ViewHiring();
                viewHiring.Companies = companymodel;
                viewHiring.Hirings = hiringmodel;
                viewHiring.Session = sessionmodel;
               
                
                return View(viewHiring);
            }
            else
            {
                TempData["serror"] = "You have to login to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult EditHiring(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1 || @context.HttpContext.Session.GetInt32("role") == 2)
            {
                Hiring model = new Hiring();
                var companymodel = new List<Company>();
                var departmentmodel = new List<Department>();
                var sectormodel = new List<Sector>();
                var hiringdepartmentmodel = new List<Hiring_Departments>();
                var hiringsectormodel = new List<Hiring_sectors>();
                var sessionmodel = new List<Sessions>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "gethiringdetails&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var hirings = JsonDecode.FromJson(data);
                    model = hirings.Hiring[0];

                }
                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
                if (response1.IsSuccessStatusCode)
                {
                    String data = response1.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);
                    foreach (var company in companies.Companies)
                    {
                        var Company = new Company
                        {
                            id = company.id,
                            name = company.name
                        };
                        companymodel.Add(Company);
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
                HttpResponseMessage response3 = client.GetAsync(client.BaseAddress + "get_sector").Result;
                if (response3.IsSuccessStatusCode)
                {
                    String data = response3.Content.ReadAsStringAsync().Result;
                    var sector = JsonDecode.FromJson(data);
                    foreach (var sectors in sector.sectors)
                    {
                        var Sector = new Sector
                        {
                            id = sectors.id,
                            sector = sectors.sector
                        };
                        sectormodel.Add(Sector);
                    }
                }
                HttpResponseMessage response4 = client.GetAsync(client.BaseAddress + "gethiringdepartments&id=" + id).Result;
                if (response4.IsSuccessStatusCode)
                {
                    String data = response4.Content.ReadAsStringAsync().Result;
                    var hiringdepartments = JsonDecode.FromJson(data);
                    foreach (var hiringdepartment in hiringdepartments.Hiring_Departments)
                    {
                        hiringdepartmentmodel.Add(hiringdepartment);

                    }
                    Debug.WriteLine(hiringdepartmentmodel);
                }
                HttpResponseMessage response5 = client.GetAsync(client.BaseAddress + "gethiringsectors&id=" + id).Result;
                if (response5.IsSuccessStatusCode)
                {
                    String data = response5.Content.ReadAsStringAsync().Result;
                    var hiringsectors = JsonDecode.FromJson(data);
                    foreach (var hiringsector in hiringsectors.Hiring_sectors)
                    {
                        hiringsectormodel.Add(hiringsector);
                    }
                    Debug.WriteLine(hiringsectormodel);
                }
                ViewCompanySession ViewCompnaySession = new ViewCompanySession();
                ViewCompnaySession.Companies = companymodel;
                ViewCompnaySession.Hiring = model;
                ViewCompnaySession.Departments = departmentmodel;
                ViewCompnaySession.Sectors = sectormodel;
                ViewCompnaySession.Hiring_Departments = hiringdepartmentmodel;
                ViewCompnaySession.Hiring_sectors = hiringsectormodel;
                ViewCompnaySession.Sessions = sessionmodel;
                return View(ViewCompnaySession);
            }
            else
            {
                TempData["serror"] = "You have to login to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost]
        public IActionResult EditingHiring(ViewCompanySession model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                /*if (ModelState.IsValid)
                {*/
                    String data = JsonConvert.SerializeObject(model);
                    Debug.WriteLine(data);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "updatehiringdetails&id=" + model.Hiring.id, content).Result;
                if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        Debug.WriteLine(result);
                        TempData["success"] = "Hiring successfully Updated.";
                        return RedirectToAction("CordHiringCompanies");
                    }
                /*}*/
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult DeleteHiring(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "deletehiringdetails&id=" + id).Result;
                TempData["success"] = "Hiring successfully deleted.";
                return RedirectToAction("CordHiringCompanies");
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult StudentApplications()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<Appliedcompany> model = new List<Appliedcompany>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getappliedcompanies").Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var companies = JsonDecode.FromJson(data);

                    if (companies.appliedcompanies != null)
                    {
                        foreach (var company in companies.appliedcompanies)
                        {
                            model.Add(company);
                        }
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

        public IActionResult StdAppList(int hid)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                List<pendingshortlist> model = new List<pendingshortlist>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getappliedstudents&hid=" + hid).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(data);
                    var appliedstudent = JsonDecode.FromJson(data);
                    if (appliedstudent.Success)
                    {
                        foreach (var student in appliedstudent.appliedstudent)
                        {
                            model.Add(student);
                        }
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

        public IActionResult pendingshortlisted(int id, int sid, int hid, IFormCollection collection)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 2)
            {
                var min_stipend = collection["minsti"];
                var max_stipend = collection["maxsti"];
                var min_salary = collection["minsal"];
                var max_salary = collection["maxsal"];
                var doj = collection["joining"];
                var pairs = new Dictionary<string, string>
                            {
                                { "min_stipend", min_stipend },
                                { "max_stipend", max_stipend },
                                { "min_salary", min_salary },
                                { "max_salary", max_salary },
                                { "doj", doj}
                            };
                String data = JsonConvert.SerializeObject(pairs);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "pendingshortlist&sid=" + sid + "&id=" + id + "&hid=" + hid, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String res = response.Content.ReadAsStringAsync().Result;
                    var status = JsonDecode.FromJson(res);
                    TempData["success"] = status.status;
                    return RedirectToAction("StudentApplications");
                }
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpGet]
        public IActionResult StudentJoiningData(int hid, int sid)
        {
            List<StudentApplication> students = new List<StudentApplication>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "get_student_application&sid=" + sid + "&hid=" + hid).Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                var student = JsonDecode.FromJson(data);
                if (student.Success)
                {
                    foreach (var std in student.applications)
                    {
                        students.Add(std);
                    }
                }
            }
            return PartialView("_StudentJoiningData", students);
        }
    }
}
