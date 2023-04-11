using Microsoft.AspNetCore.Mvc;

namespace Demo.Models
{
    public class Hiring 
    {
        public long id { get; set; }
        public long company_id { get; set; }    
        public long session_id { get;set; }
        public string designation { get; set; }
        public string bond { get; set; }
        public string bond_condition { get; set; }
        public double fix_salary { get; set; }
        public double bonus { get; set; }
        public double performance_inc { get; set; }
        public double total_salary { get; set; }
        public string joblocation { get; set; }

        public DateOnly joindate { get; set; }  
        public DateOnly startdate { get; set; }  
        public DateOnly enddate { get; set; }
        public string interview_mode { get; set; }
        public string interview_location { get; set; }
        public string other_requirement { get; set; }
        public int status { get; set; }
        public string remarks { get; set; }




    }
}
