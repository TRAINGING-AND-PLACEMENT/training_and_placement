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
        private readonly IEmailSender emailSender;
        HttpClient client;
        private readonly IHttpContextAccessor context;
        public UserController(IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
        {
            Webapi wb = new Webapi();
            System.Uri baseAddress = wb.api();
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            context = httpContextAccessor;
            this.emailSender = emailSender;
        }
        public IActionResult Role()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Role(int id)
        {
            if(id == 1)
            {
                return RedirectToAction("Login", new { id = 1 });
            }
            else if(id == 2)
            {
                return RedirectToAction("Login", new { id = 2 });
            }
            else
            {
                TempData["error"] = "press any button";
                return RedirectToAction("Role");
            }
        }
        public IActionResult Login(int id)
        {
            if (id == 1)
            {
                TempData["r"] = id;
                return View();
            }
            else if (id == 2)
            {
                TempData["r"] = id;
                return View();
            }
            else
            {
                TempData["error"] = "choose any role to sign in.";
                return RedirectToAction("Role");
            }
        }
        [HttpPost]
        public IActionResult Login(User model,int id)
        {
            if (id == 1 || id == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "getlogin", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        var logindetails = JsonDecode.FromJson(result);
                        var success = logindetails.Success;
                        if (success)
                        {
                            int role = Convert.ToInt32(logindetails.User[0]["role"]);
                            if (id == role)
                            {
                                if (role == 1)
                                {
                                    int userid = Convert.ToInt32(logindetails.User[0]["id"]);
                                    int sessionid = Convert.ToInt32(logindetails.Student[0]["session_id"]);
                                    int studentid = Convert.ToInt32(logindetails.Student[0]["id"]);
                                    context.HttpContext.Session.SetInt32("role", role);
                                    context.HttpContext.Session.SetInt32("userid", userid); 
                                    context.HttpContext.Session.SetInt32("sessionid", sessionid);
                                    context.HttpContext.Session.SetInt32("studentid", studentid);
                                    return RedirectToAction("StudentProfile", "Student");
                                }
                                else if (role == 2)
                                {
                                    int userid = Convert.ToInt32(logindetails.User[0]["id"]);
                                    int sessionid = Convert.ToInt32(logindetails.Sessions[0]["id"]);
                                    context.HttpContext.Session.SetInt32("role", role);
                                    context.HttpContext.Session.SetInt32("userid", userid);
                                    context.HttpContext.Session.SetInt32("sessionid", sessionid);
                                    return RedirectToAction("Index", "Company");
                                }
                            }
                            else
                            {
                                if (id == 1)
                                {
                                    TempData["error"] = "Enter the credentials of a student!";
                                    TempData["r"] = id;
                                    return RedirectToAction("Login");
                                }
                                else if (id == 2)
                                {
                                    TempData["error"] = "Enter the credentials of a co-ordinator!";
                                    TempData["r"] = id;
                                    return RedirectToAction("Login");
                                }
                            }
                        }
                        else
                        {
                            TempData["error"] = "Wrong id or password";
                            TempData["r"] = id;
                            return RedirectToAction("Login");
                        }
                    }
                }
                return View(model);
            }
            else { TempData["error"] = "Please select role to sign in!"; return RedirectToAction("Role"); }
            
        }
        public IActionResult Logout()
        {
            context.HttpContext.Session.Remove("role");
            context.HttpContext.Session.Remove("userid");
            context.HttpContext.Session.Remove("sessionid");
            context.HttpContext.Session.Remove("studentid");
            return RedirectToAction("Login");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("ChangePassword");
            }
            return View(model);
        }
    }
}
