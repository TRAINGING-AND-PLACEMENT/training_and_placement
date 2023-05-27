using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Demo.Controllers
{
    public class ExportController : Controller
    {
        private readonly IHttpContextAccessor context;
        HttpClient client;
        public ExportController(IHttpContextAccessor httpContextAccessor)
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
        private DataTable GetStudentDetails()
        {
            List<Student> model = new List<Student>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail").Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(data);
                var students = JsonDecode.FromJson(data);
                foreach (var student in students.students)
                {
                    model.Add(student);
                }
            }

            var std = model.ToList();

            DataTable dtstudent = new DataTable("StudentDetails");
            dtstudent.Columns.AddRange(new DataColumn[] {   new DataColumn("Name"),
                                                            new DataColumn("Enrollment"),
                                                            new DataColumn("Gender"),
                                                            new DataColumn("Contact"),
                                                            new DataColumn("Address"), 
                                                            new DataColumn("Pincode"), 
                                                            new DataColumn("Adhaar"), 
                                                            new DataColumn("Pan"),
                                                            new DataColumn("DL"),
                                                            new DataColumn("Tenth Passyear"),
                                                            new DataColumn("Tenth Score"),
                                                            new DataColumn("Twelve Passyear"),
                                                            new DataColumn("Twelve Score"),
                                                            new DataColumn("Ug Passyear"),
                                                            new DataColumn("Ug Score"),
                                                            new DataColumn("Pg Passyear"),
                                                            new DataColumn("Pg Score")
                                                        });
            foreach (var student in std)
            {
                dtstudent.Rows.Add( student.surname + student.first_name + student.last_name,
                                    student.enrollment, student.gender, student.contact,
                                    student.address+","+student.city+","+student.state,
                                    student.pincode,student.adhaar,student.pan,student.driving,
                                    student.ten_passyear,student.ten_score+" out of "+student.ten_scoreoutof,
                                    student.twelve_passyear, student.twelve_score + " out of " + student.twelve_scoreoutof,
                                    student.ug_passyear, student.ug_score + " out of " + student.ug_scoreoutof,
                                    student.pg_passyear, student.pg_score + " out of " + student.pg_scoreoutof);
            }

            return dtstudent;
        }
        private void ExportToCsv(DataTable students)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = students.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in students.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(",", fields));
            }
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
            Response.Clear();
            Response.Headers.Add("content-disposition", "attachment;filename=Studentdetails.csv");
            Response.ContentType = "application/text";
            Response.Body.WriteAsync(byteArray);
            Response.Body.Flush();
        }

        public IActionResult ExporDataToFile()
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            var products = GetStudentDetails();
            switch (exportType)
            {
                /*case "Excel":
                    ExportToExcel(products);
                    break;*/
                case "Csv":
                    ExportToCsv(products);
                    break;
                /*case "Pdf":
                    ExportToPdf(products);
                    break;
                case "Word":
                    ExportToWord(products);
                    break;
                case "Json":
                    ExportToJson(products);
                    break;
                case "Xml":
                    ExportToXML(products);
                    break;
                case "Text":
                    ExportToText(products);
                    break;*/
            }
            return null;
        }
    }
}
