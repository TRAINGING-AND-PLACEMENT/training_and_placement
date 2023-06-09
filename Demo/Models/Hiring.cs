﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class Hiring 
    {
        [Key]
        [JsonProperty("id")]
        public long id { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "{0} is Required.")]
        [JsonProperty("company_id")]
        public long company_id { get; set; }

        [DisplayName("Session Name")]
        [Required(ErrorMessage = "{0} is Required.")]
        [JsonProperty("session_id")]
        public long session_id { get;set; }

        [DisplayName("Department Id")]
        [Required(ErrorMessage = "You must provide a Department Name!")]
        [JsonProperty("department_id")]
        public List<string> department_id { get; set; }

        [DisplayName("Sector Id")]
        [Required(ErrorMessage = "You must provide a Sector Name!")]
        [JsonProperty("sector_id")]
        public List<string> sector_id { get; set; }

        [DisplayName("Designation")]
        [Required(ErrorMessage = "You must provide a Designation!")]
        [JsonProperty("designation")]
        public string designation { get; set; }

        [DisplayName("Bond")]
        [Required(ErrorMessage = "You must provide a Bond!")]
        [JsonProperty("bond")]
        public string bond { get; set; }

        [DisplayName("Bond Condition")]
        [ValidateNever]
        [JsonProperty("bond_condition")]
        public string bond_condition { get; set; }

        [Required(ErrorMessage = "You must provide a Stipend !")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Not a valid number")]
        [JsonProperty("min_stipend")]
        public double min_stipend { get; set; }

        [Required(ErrorMessage = "You must provide a Stipend !")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Not a valid number")]
        [JsonProperty("max_stipend")] 
        public double max_stipend { get; set; }

        [Required(ErrorMessage = "You must provide a minimum package!")]
        [JsonProperty("minimum_package")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Not a valid number")]
        [DisplayName("Minimum Salary")]
        public string minimum_package { get; set; }

        [Required(ErrorMessage = "You must provide a maximum package !")]
        [JsonProperty("maximum_package")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Not a valid number")]
        [DisplayName("Maximum Salary")]
        public string maximum_package { get; set; }

        [Required(ErrorMessage = "You must provide a bonus")]
        [JsonProperty("bonus")]
        public double bonus { get; set; }


        [Required(ErrorMessage = "You must provide a performance incentives")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Not a valid number")]
        [JsonProperty("performance_inc")]
        public double performance_inc { get; set; }


        [Required(ErrorMessage = "You must provide a joblocation")]
        [JsonProperty("joblocation")]
        public string joblocation { get; set; }

        [Required(ErrorMessage = "You must provide a joindate")]
        [JsonProperty("joindate")]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyy}",ApplyFormatInEditMode =true)]
        public string joindate { get; set; }

        [Required(ErrorMessage = "You must provide a startdate")]
        [JsonProperty("startdate")]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyy}", ApplyFormatInEditMode = true)]
        public string startdate { get; set; }

        [Required(ErrorMessage = "You must provide a enddate")]
        [JsonProperty("enddate")]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyy}", ApplyFormatInEditMode = true)]
        public string enddate { get; set; }

        [Required(ErrorMessage = "You must provide a interview mode")]
        [JsonProperty("interview_mode")]
        public string interview_mode { get; set; }

        [Required(ErrorMessage = "You must provide a interview location")]
        [JsonProperty("interview_location")]
        public string interview_location { get; set; }

       
        [JsonProperty("other_requirement")]
        [Required(ErrorMessage = "You must provide a Other Requirement")]
        public string other_requirement { get; set; }

        [ValidateNever]
        public int status { get; set; }

        [ValidateNever]
        public string remarks { get; set; }
    }
}
