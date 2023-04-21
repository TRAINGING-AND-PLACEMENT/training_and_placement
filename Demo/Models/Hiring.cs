using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Hiring 
    {
        [Key]
        [JsonProperty("id")]
        public long id { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "You must provide a Company Name!")]
        [JsonProperty("company_id")]
        public long company_id { get; set; }

        [DisplayName("Session Name")]
        [Required(ErrorMessage = "You must provide a Session Name!")]
        [JsonProperty("session_id")]
        public long session_id { get;set; }

        [DisplayName("Designation")]
        [Required(ErrorMessage = "You must provide a identification number!")]
        [JsonProperty("designation")]
        public string designation { get; set; }

        [DisplayName("Bond")]
        [Required(ErrorMessage = "You must provide a Bond!")]
        [JsonProperty("bond")]
        public string bond { get; set; }

        [DisplayName("Bond Condition")]
        [Required(ErrorMessage = "You must provide a Bond Condition!")]
        [JsonProperty("bond_condition")]
        public string bond_condition { get; set; }

        [Required(ErrorMessage = "You must provide a Fix Salary!")]
        [RegularExpression(@"^\(?([0-9])$", ErrorMessage = "Not a valid number")]
        [JsonProperty("fix_salary")]
        public double fix_salary { get; set; }

        [Required(ErrorMessage = "You must provide a bonus")]
        [RegularExpression(@"^\(?([0-9])$", ErrorMessage = "Not a valid number")]
        [JsonProperty("bonus")]
        public double bonus { get; set; }


        [Required(ErrorMessage = "You must provide a performance incentives")]
        [JsonProperty("performance_inc")]
        public double performance_inc { get; set; }

        [Required(ErrorMessage = "You must provide a Total Salary")]
        [RegularExpression(@"^\(?([0-9])$", ErrorMessage = "Not a valid number")]
        [JsonProperty("total_salary")]
        public double total_salary { get; set; }

        [Required(ErrorMessage = "You must provide a joblocation")]
        [JsonProperty("joblocation")]
        public string joblocation { get; set; }

        [Required(ErrorMessage = "You must provide a joindate")]
        [JsonProperty("joindate")]
        public DateOnly joindate { get; set; }

        [Required(ErrorMessage = "You must provide a startdate")]
        [JsonProperty("startdate")]
        public DateOnly startdate { get; set; }

        [Required(ErrorMessage = "You must provide a enddate")]
        [JsonProperty("enddate")]
        public DateOnly enddate { get; set; }

        [Required(ErrorMessage = "You must provide a interview mode")]
        [JsonProperty("interview_mode")]
        public string interview_mode { get; set; }

        [Required(ErrorMessage = "You must provide a interview location")]
        [JsonProperty("interview_location")]
        public string interview_location { get; set; }

        [Required(ErrorMessage = "You must provide a other requirements")]
        [JsonProperty("other_requirement")]
        public string other_requirement { get; set; }


        public int status { get; set; }
        public string remarks { get; set; }
    }
}
