using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;    

namespace Demo.Models
{
    public class AdditionalQualif
    {
        [Key]
        [JsonProperty("id")]
        public int id { get; set; }

        [DisplayName("Student Id")]
        [Required(ErrorMessage = "You must provide a student name!")]
        [JsonProperty("student_id")]
        public int student_id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "You must provide a title!")]
        [JsonProperty("title")]
        public string title { get; set; }

        [DisplayName("Specialization")]
        [Required(ErrorMessage = "You must provide specialization!")]
        [JsonProperty("specialization")]
        public string specialization { get; set; }

        [DisplayName("Other Details")]
        [Required(ErrorMessage = "You must provide other information!")]
        [JsonProperty("other")]

        public string other { get; set; }

        [DisplayName("Total Marks")]
        [Required(ErrorMessage = "You must provide total marks!")]
        [JsonProperty("score")]
        public string score { get; set; }

        [DisplayName("Marks Scored")]
        [Required(ErrorMessage = "You must provide marks obtained out of total marks!")]
        [JsonProperty("scoreoutof")]
        public string scoreoutof { get; set;}

        [ValidateNever]
        public string status { get; set; }

        [DisplayName("remarks")]
        [ValidateNever]
        [JsonProperty("remarks")]
        public string remarks { get; set; }

        [ValidateNever]
        public string created_at { get; set; }

        [ValidateNever]
        public string updated_at { get; set; }

    }
}
