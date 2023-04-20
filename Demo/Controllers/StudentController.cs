﻿using Azure.Core;
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
            return View();
        }
        public IActionResult View12thData()
        {
            return View(); 
        }
        public IActionResult ViewUGData()
        {
            return View();
        }
        public IActionResult ViewPGData()
        {
            return View();
        }
    }
}