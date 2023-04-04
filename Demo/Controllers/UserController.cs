using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Text;

namespace Demo.Controllers
{
    public class UserController : Controller
    {
        HttpClient client;
        JsonView jv = new JsonView();
        private readonly IHttpContextAccessor context;
        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = httpContextAccessor;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model) 
        {
            if (ModelState.IsValid)
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "getlogin", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    Login logindetails = jv.LoginDetails(result);
                    var success = logindetails.success;
                    if (success)
                    {
                        int role = Convert.ToInt32(logindetails.user.role);
                        if(role == 1)
                        {
                            int userid = Convert.ToInt32(logindetails.user.id);
                            int sessionid = Convert.ToInt32(logindetails.student.session_id);
                            int studentid = Convert.ToInt32(logindetails.student.id);
                            context.HttpContext.Session.SetInt32("role", role);
                            context.HttpContext.Session.SetInt32("userid", userid);
                            context.HttpContext.Session.SetInt32("sessionid", sessionid);
                            context.HttpContext.Session.SetInt32("studentid", studentid);
                            return RedirectToAction("StudentProfile", "Student");
                        }
                        else if (role == 2)
                        {
                            int userid = Convert.ToInt32(logindetails.user.id);
                            int sessionid = Convert.ToInt32(logindetails.sessions.id);
                            context.HttpContext.Session.SetInt32("role", role);
                            context.HttpContext.Session.SetInt32("userid", userid);
                            context.HttpContext.Session.SetInt32("sessionid", sessionid);
                            return RedirectToAction("Index","Company");
                        }
                    }
                    else
                    {
                        TempData["error"] = "Wrong id or password";
                        return RedirectToAction("Login");
                    }
                }
            }
            return View(model);
        }
    }
}
