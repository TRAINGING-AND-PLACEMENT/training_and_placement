using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class Session_insert
    {
        [ValidateNever]
        [AllowNull]
        public int id { get; set; }

        [Required(ErrorMessage = "You must provide a startdate")]
        [JsonProperty("start_date")]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyy}", ApplyFormatInEditMode = true)]
        public string start_date { get; set; }

        [Required(ErrorMessage = "You must provide a enddate")]
        [JsonProperty("end_date")]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyy}", ApplyFormatInEditMode = true)]
        public string end_date { get; set; }

        [Required]
        public string label { get; set; }

        [ValidateNever]
        [AllowNull]
        public int default_year { get; set; } = 0;

        [ValidateNever]
        public int status { get; set; } = 0;

        [ValidateNever]
        public string remarks { get; set; }

        [ValidateNever]
        public string created_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");

        [ValidateNever]
        public string updated_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
    }
}

