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
		JsonView cj = new JsonView();
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

        public IActionResult StudentProfile(int sid, int uid) {
            if (@context.HttpContext.Session.GetInt32("role") == 1)
            {
				Student model = new Student();
				HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentalldetails&sid=" + @context.HttpContext.Session.GetInt32("studentid") +"&uid="+  @context.HttpContext.Session.GetInt32("userid")).Result;
				if (response.IsSuccessStatusCode)
                { 
					String data = response.Content.ReadAsStringAsync().Result;
					var res = cj.Ustudent(data);
                    var sres =  res.Split(",");
                    Debug.Write(sres[0]);
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

       

        public IActionResult AlterStudent()
        {
            return View();
        }

        public IActionResult ViewTenthData()
        {
            return View();
        }
    }
}
