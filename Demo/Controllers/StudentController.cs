using Azure.Core;
using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Text;

namespace Demo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
		public StudentController(IHttpContextAccessor httpContextAccessor)
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
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult StudentProfile(int id) {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
				Student model = new Student();
				HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
				if (response.IsSuccessStatusCode)
                { 
					String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
				return View(model);
			}
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult Edit_Student(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult update_student(Student model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "updatestudentdetails&id="+@context.HttpContext.Session.GetInt32("studentid")  , content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("StudentProfile");
                }
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult ViewTenthData()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost]
        public IActionResult EditTenthData(Student model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "editten_details&id=" + @context.HttpContext.Session.GetInt32("studentid"), content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ViewTenthData");
                }
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult View12thData()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult edit_twelve_data(Student model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "editstudent_twelve_data&id=" + @context.HttpContext.Session.GetInt32("studentid"), content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("View12thData");
                }
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult ViewUGData()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult edit_ug_data(Student model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "editstudent_ug_data&id=" + @context.HttpContext.Session.GetInt32("studentid"), content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ViewUGData");
                }
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }


        public IActionResult ViewPGData()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult edit_pg_data(Student model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "editstudent_pg_data&id=" + @context.HttpContext.Session.GetInt32("studentid"), content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ViewPGData");
                }
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult Edit10Details()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult Edit12Details()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult EditUGDetails()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult EditPGDetails()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Student model = new Student();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&id=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var res = JsonDecode.FromJson(data);
                    model = res.studentInfo[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult ViewInternships()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                List<Internships> model = new List<Internships>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getinternship&sid="+ @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var internships = JsonDecode.FromJson(data);
                    if (internships.internships != null)
                    {
                        foreach (var internship in internships.internships)
                        {
                            model.Add(internship);
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
        public IActionResult AddInternship()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost]
        public IActionResult AddInternship(Internships model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                Debug.WriteLine(data);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "addInternship", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ViewInternships");
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult EditInternship(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                Internships model = new Internships();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getinternship&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var intership = JsonDecode.FromJson(data);
                    model = intership.internships[0];
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
        public IActionResult EditInternship(Internships model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
            
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "updateinternship&id=" + model.id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ViewInternships");
                }
                return View();
                
            }
            else
            {
                TempData["serror"] = "You have to login with co-ordinator id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult ViewWorkExp()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                List<work_experiences> model = new List<work_experiences>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getWorkExperiances&sid=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var workExpe = JsonDecode.FromJson(data);
                    if (workExpe.workExperiances != null)
                    {
                        foreach (var experiance in workExpe.workExperiances)
                        {
                            model.Add(experiance);
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

        public IActionResult AddWorkExp()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult AddWorkExp(work_experiences model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                Debug.WriteLine(data);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "addWorkExperiance", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    Debug.Write(result);
                    return RedirectToAction("ViewWorkExp");
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult EditWorkExp(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                work_experiences model = new work_experiences();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getWorkExperiances&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var work_expe = JsonDecode.FromJson(data);
                    model = work_expe.workExperiances[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult EditWorkExp(work_experiences model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {

                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "updateWorkExperiance&id=" + model.id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ViewWorkExp");

                }
                return View();

            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult ViewAQData()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                List<AdditionalQualif> model = new List<AdditionalQualif>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getAddQulification&sid=" + @context.HttpContext.Session.GetInt32("studentid")).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var addQuali = JsonDecode.FromJson(data);
                    if (addQuali.additionalQualifications != null)
                    {
                        foreach (var qualification in addQuali.additionalQualifications)
                        {
                            model.Add(qualification);
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

        public IActionResult AddAQData()
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult AddAQData(AdditionalQualif model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "insertAddtionalQuali", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ViewAQData");
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
        }
        public IActionResult EditAQData(int id)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                AdditionalQualif model = new AdditionalQualif();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getAddQulification&id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String data = response.Content.ReadAsStringAsync().Result;
                    var Aqdata = JsonDecode.FromJson(data);
                    model = Aqdata.additionalQualifications[0];
                }
                return View(model);
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }
            
        }

        [HttpPost]
        public IActionResult EditAQData(AdditionalQualif model)
        {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "updateAddtionalQualification&id=" + model.id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("ViewAQData");

                }
                return View();
            }
            else
            {
                TempData["serror"] = "You have to login with student id and password to access the page.";
                DestorySession();
                return RedirectToAction("Login", "User");
            }

        }
    }
}
