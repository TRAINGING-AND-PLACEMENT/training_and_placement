using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
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
        private DataTable GetStudentDetails(int cid)
        {
            List<Student> model = new List<Student>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getstudentdetail&cid="+cid).Result;
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
                                                            new DataColumn("Date Of Birth"),
                                                            new DataColumn("Contact"),
                                                            new DataColumn("Parent Contact"),
                                                            new DataColumn("Address"),
                                                            new DataColumn("Pincode"),
                                                            new DataColumn("Permament Address"),
                                                            new DataColumn("Permament Pincode"),
                                                            new DataColumn("Blood Group"),
                                                            new DataColumn("Adhaar"),
                                                            new DataColumn("Pan"),
                                                            new DataColumn("DL"),
                                                            new DataColumn("English language"),
                                                            new DataColumn("Hindi language"),
                                                            new DataColumn("Gujarati language"),
                                                            new DataColumn("Marathi language"),
                                                            new DataColumn("Other language"),
                                                            new DataColumn("Tenth School"),
                                                            new DataColumn("Tenth School Address"),
                                                            new DataColumn("Tenth School Pincode"),
                                                            new DataColumn("Tenth School board"),
                                                            new DataColumn("Tenth Passyear"),
                                                            new DataColumn("Tenth Score"),
                                                            new DataColumn("Tenth Gap Number"),
                                                            new DataColumn("Tenth Gap Years"),
                                                            new DataColumn("Tenth Admission Quota"),
                                                            new DataColumn("Twelve School"),
                                                            new DataColumn("Twelve School Address"),
                                                            new DataColumn("Twelve School Pincode"),
                                                            new DataColumn("Twelve School board"),
                                                            new DataColumn("Twelve Passyear"),
                                                            new DataColumn("Twelve Score"),
                                                            new DataColumn("Twelve Gap Number"),
                                                            new DataColumn("Twelve Gap Years"),
                                                            new DataColumn("Twelve Admission Quota"),
                                                            new DataColumn("Twelve Specialization"),
                                                            new DataColumn("Ug Degree"),
                                                            new DataColumn("Ug College"),
                                                            new DataColumn("Ug Address"),
                                                            new DataColumn("Ug Pincode"),
                                                            new DataColumn("Ug University"),
                                                            new DataColumn("Ug Passyear"),
                                                            new DataColumn("Ug Score"),
                                                            new DataColumn("Ug Gap Number"),
                                                            new DataColumn("Ug Gap Years"),
                                                            new DataColumn("Pg Degree"),
                                                            new DataColumn("Pg College"),
                                                            new DataColumn("Pg Address"),
                                                            new DataColumn("Pg Pincode"),
                                                            new DataColumn("Pg University"),
                                                            new DataColumn("Pg Passyear"),
                                                            new DataColumn("Pg Score"),
                                                            new DataColumn("Pg Gap Number"),
                                                            new DataColumn("Pg Gap Years")
                                                        });
            foreach (var student in std)
            {
                dtstudent.Rows.Add(student.surname + student.first_name + student.last_name,
                                    student.enrollment, student.gender, student.dob, student.contact, student.parent_contact,
                                    student.address + "," + student.city + "," + student.state,
                                    student.pincode, student.permanent_address + "," + student.permanent_city + "," + student.permanent_state, student.permanent_pincode,
                                    student.blood_group, student.adhaar, student.pan, student.driving, student.lang_eng, student.lang_hindi, student.lang_guj, student.lang_marathi, student.lang_other,
                                    student.ten_school, student.ten_schooladdress + "," + student.ten_schoolcity, student.ten_schoolpincode, student.ten_board, student.ten_passyear, student.ten_score + " out of " + student.ten_scoreoutof, student.ten_gapno, student.ten_gapyears, student.ten_admissionquota,
                                    student.twelve_school, student.twelve_schooladdress + "," + student.twelve_schoolcity, student.twelve_schoolpincode, student.twelve_board, student.twelve_passyear, student.twelve_score + " out of " + student.twelve_scoreoutof, student.twelve_gapno, student.twelve_gapyears, student.twelve_admissionquota, student.twelve_specialization,
                                    student.ug_degree, student.ug_college, student.ug_collegeaddress + "," + student.ug_collegecity, student.ug_collegepincode, student.ug_university, student.ug_passyear, student.ug_score + " out of " + student.ug_scoreoutof, student.ug_gapno, student.ug_gapyears,
                                    student.pg_degree, student.pg_college, student.pg_collegeaddress + "," + student.pg_collegecity, student.pg_collegepincode, student.pg_university, student.pg_passyear, student.pg_score + " out of " + student.pg_scoreoutof, student.pg_gapno, student.pg_gapyears);
            }

            return dtstudent;
        }
        private DataTable GetStudentUser()
        {
            List<StudentUser> model = new List<StudentUser>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "getuserdetails").Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(data);
                var users = JsonDecode.FromJson(data);
                foreach (var user in users.StudentUsers)
                {
                    model.Add(user);
                }
            }

            var ustd = model.ToList();

            DataTable userdtstudent = new DataTable("StudentUser");
            userdtstudent.Columns.AddRange(new DataColumn[] {new DataColumn("Batch"),
                                                            new DataColumn("Department"),      
                                                            new DataColumn("Name"),
                                                            new DataColumn("Email"),
                                                            new DataColumn("Password"),
                                                            new DataColumn("Role"),
                                                        });
            string role = "";
            foreach (var users in ustd)
            {
                if (users.role == 1)
                {
                    role = "Student";
                }
                userdtstudent.Rows.Add(users.label, users.department, users.name, users.email, users.password, role);

            }

            return userdtstudent;
        }
        private DataTable GetStudentApplicationReport(int sid, int did, int cid, int stid,string include)
        {
            List<StudentReport> students = new List<StudentReport>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "filterstudent&sid=" + sid + "&did=" + did + "&cid=" + cid + "&stid=" + stid + "&include").Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                var student = JsonDecode.FromJson(data);
                if (student.Success)
                {
                    foreach (var std in student.studentReport)
                    {
                        students.Add(std);
                    }
                }
            }
            var report = students.ToList();
            DataTable dtstdreport = new DataTable("StudentUser");
            dtstdreport.Columns.AddRange(new DataColumn[] { new DataColumn("Batch"),
                                                            new DataColumn("Department"),
                                                            new DataColumn("Enrollment"),
                                                            new DataColumn("Name"),
                                                            new DataColumn("Email"),
                                                            new DataColumn("Contact"),
                                                            new DataColumn("Company"),
                                                            new DataColumn("Stipend"),
                                                            new DataColumn("Salary"),
                                                            new DataColumn("Status")
                                                        });

            var status = "";
            foreach (var r in report)
            {
                if (r.status == 0)
                {
                    status = "Pending";
                }
                else if (r.status == 1)
                {
                    status = "Selected";
                }
                else if (r.status == 2)
                {
                    status = "Rejected";
                }
                dtstdreport.Rows.Add(r.label, r.department_name, r.enrollment, r.student_surname + " " + r.student_firstname + " " + r.student_lastname, r.student_email, r.student_contact,
                                        r.company_name, r.stipend, r.salary, status);

            }

            return dtstdreport;
        }
        private void ExportToCsv(DataTable getanydata)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = getanydata.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in getanydata.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field =>
                  string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                sb.AppendLine(string.Join(",", fields));
            }
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
            Response.Clear();
            Response.Headers.Add("content-disposition", "attachment;filename=ExportFile.csv");
            Response.ContentType = "application/text";
            Response.Body.WriteAsync(byteArray);
            Response.Body.Flush();
        }

        public IActionResult ExportDataToFile(String Export, int sid, int did, int cid, int stid, string include)
        {
            //var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            //var exportType = dictioneryexportType["Export"];
            var exportType = Export;
            var getanydata = new DataTable();
            switch (exportType)
            {
                case "appliedcompanystudent":
                    getanydata = GetStudentDetails(cid);
                    ExportToCsv(getanydata);
                    break;
                case "studentuser":
                    getanydata = GetStudentUser();
                    ExportToCsv(getanydata);
                    break;
                case "StdAppReport":
                    getanydata = GetStudentApplicationReport(sid, did, cid, stid, include);
                    ExportToCsv(getanydata);
                    break;
            }
            return null;
        }
        public IActionResult ReportView()
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
                        label = ss.label
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

            List<Company> companies = new List<Company>();
            HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "getcompanydetails").Result;
            if (response2.IsSuccessStatusCode)
            {
                String data = response2.Content.ReadAsStringAsync().Result;
                var company = JsonDecode.FromJson(data);
                foreach (var cm in company.Companies)
                {
                    companies.Add(new Company
                    {
                        id = cm.id,
                        name = cm.name
                    });
                }
            }
            List<StudentReport> students = new List<StudentReport>();
            HttpResponseMessage studentresponse = client.GetAsync(client.BaseAddress + "filterstudent").Result;
            if (studentresponse.IsSuccessStatusCode)
            {
                String data = studentresponse.Content.ReadAsStringAsync().Result;
                var student = JsonDecode.FromJson(data);
                if (student.Success)
                {
                    foreach (var std in student.studentReport)
                    {
                        students.Add(std);
                    }
                }
            }

            ViewBag.sessiondd = sessions;
            ViewBag.deptdd = departments;
            ViewBag.compdd = companies;
            return View(students);
        }

        [HttpPost]
        public IActionResult FilterStudent(int sid, int did, int cid, int stid)
        {
            List<StudentReport> students = new List<StudentReport>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "filterstudent&sid="+sid+"&did="+did+"&cid="+cid+"&stid="+stid+"&include").Result;
            if (response.IsSuccessStatusCode)
            {
                String data = response.Content.ReadAsStringAsync().Result;
                var student = JsonDecode.FromJson(data);
                if (student.Success)
                { 
                    foreach (var std in student.studentReport)
                    {
                        students.Add(std);
                    }
                }
            }
            return PartialView("_StudentReport", students);
        }
        
    }
}
