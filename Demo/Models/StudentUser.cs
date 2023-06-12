using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Demo.Models
{
    public class StudentUser
    {
        public int id { get; set; }

        
        public string name { get; set; }

      
        public string email { get; set; }

       
        public string password { get; set; }

      
        public int role { get; set; }

        
        public string status { get; set; }

        public string remarks { get; set; }

       
        public string created_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");

       
        public string updated_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");

        public string department { get; set; }

        public string label { get; set; }
    }
}
