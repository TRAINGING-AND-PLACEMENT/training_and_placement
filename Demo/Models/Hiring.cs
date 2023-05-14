﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [DisplayName("Company Id")]
        [Required(ErrorMessage = "You must provide a Company Id!")]
        [JsonProperty("company_id")]
        public long company_id { get; set; }

        [DisplayName("Session Id")]
        [Required(ErrorMessage = "You must provide a Session Id!")]
        [JsonProperty("session_id")]
        public long session_id { get;set; }

        [DisplayName("Department Id")]
        [Required(ErrorMessage = "You must provide a Department Id!")]
        [JsonProperty("department_id")]
        public long department_id { get; set; }

        [DisplayName("Sector Id")]
        [Required(ErrorMessage = "You must provide a Sector Id!")]
        [JsonProperty("sector_id")]
        public long sector_id { get; set; }

        [DisplayName("Designation")]
        [Required(ErrorMessage = "You must provide a Designation!")]
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
        [RegularExpression(@"^\d${9}", ErrorMessage = "Not a valid number")]
        [JsonProperty("fix_salary")]
        public double fix_salary { get; set; }

        [Required(ErrorMessage = "You must provide a bonus")]
        [RegularExpression(@"^\d${9}", ErrorMessage = "Not a valid number")]
        [JsonProperty("bonus")]
        public double bonus { get; set; }


        [Required(ErrorMessage = "You must provide a performance incentives")]
        [RegularExpression(@"^\d${9}", ErrorMessage = "Not a valid number")]
        [JsonProperty("performance_inc")]

        public double performance_inc { get; set; }

        [Required(ErrorMessage = "You must provide a Total Salary")]
        [RegularExpression(@"^\d${9}", ErrorMessage = "Not a valid number")]
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

       
        [JsonProperty("other_requirement")]
        [Required(ErrorMessage = "You must provide a Other Requirement")]
        public string other_requirement { get; set; }


        public int status { get; set; }
        public string remarks { get; set; }
    }
}
