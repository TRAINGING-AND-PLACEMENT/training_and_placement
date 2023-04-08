using Demo.Models;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text.Json.Nodes;


namespace Demo.Controllers.Json
{
    public class JsonView
    {
		public List<Student> listroot(List<Student> model, String data)
		{
			model = new List<Student>();
			RootObject root = JsonConvert.DeserializeObject<RootObject>(data);
			model = root.student;
			return model;
		}
		public Student uniroot(Student model, String data)
		{
            Root root = JsonConvert.DeserializeObject<Root>(data);
			model = root.student;
			return model;
		}
        public String Ustudent(String data)
        {
            StudentUser res = JsonConvert.DeserializeObject<StudentUser>(data);
            var result = res.student;
            return result;
        }
 		public List<Company> listroot(List<Company> model, String data)
        {
            model = new List<Company>();
            RootObject root = JsonConvert.DeserializeObject<RootObject>(data);
            model = root.result;
            return model;
        }
        public Company uniroot(Company model, String data)
        {
            Root root = JsonConvert.DeserializeObject<Root>(data);
            model = root.result;
            return model;
        }
		
		public Login LoginDetails(String data)
        {
            Login loginDetails = JsonConvert.DeserializeObject<Login>(data);
            return loginDetails;
        }
    }
    public class RootObject
    {
        public List<Company> result { get; set; }
		public List<Student> student { get; set; }
	}

    public class Root
    {
        public Company result { get; set; }
        public Student student { get; set; }
    }
    public class StudentUser
    {
        public string student { get; set; }
    }
    public class Login
    {
        public bool success { get; set; }
        public User user { get; set; }
        public Student student { get; set; }
        public Sessions sessions { get; set; }
    }
}