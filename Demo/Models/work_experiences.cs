using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class work_experiences
    {
        [Key]
        [JsonProperty("id")]
        public long id { get; set; }

        [DisplayName("Student Id")]
        [Required(ErrorMessage = "You must provide a Student Id!")]
        [JsonProperty("student_id")]
        public long student_id { get; set; }

        [DisplayName("Designation ")]
        [Required(ErrorMessage = "You must provide a designation!")]
        [JsonProperty("designation")]
        public string designation { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "You must provide a Company Name!")]
        [JsonProperty("company")]
        public string company { get; set; }

        [DisplayName("Duration")]
        [Required(ErrorMessage = "You must provide a Duration!")]
        [JsonProperty("duration")]
        public string duration { get; set; }

        [DisplayName("Pincode")]
        [Required(ErrorMessage = "You must provide a Pincode!")]
        [JsonProperty("pincode")]
        public string pincode { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "You must provide a City!")]
        [JsonProperty("city")]
        public string city { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "You must provide a Address!")]
        [JsonProperty("address")]
        public string address { get; set; }

        [DisplayName("Specialization")]
        [Required(ErrorMessage = "You must provide a Specialization!")]
        [JsonProperty("specialization")]
        public string specialization { get; set; }

        [DisplayName("Salary")]
        [Required(ErrorMessage = "You must provide a salary!")]
        [JsonProperty("salary")]
        public string salary { get; set; }

        [ValidateNever]
        [JsonProperty("status")]
        public int status { get; set; }

        [ValidateNever]
        [JsonProperty("remarks")]
        public string remarks { get; set; }

        [ValidateNever]
        [JsonProperty("created_at")]
        public string created_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");

        [ValidateNever]
        [JsonProperty("updated_at")]
        public string updated_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
    }
}
