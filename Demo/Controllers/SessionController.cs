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
    }
}
