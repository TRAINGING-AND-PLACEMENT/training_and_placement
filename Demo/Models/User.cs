using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class User
    {
        [Key]
        [Index(0)]
        [ValidateNever]
        public int id { get; set; }

        [Index(1)]
        [ValidateNever]
        public string name { get; set; }

        [Index(2)]
        [Required]
        public string email { get; set; }

        [Index(3)]
        [Required]
        public string password { get; set; }

        [Index(4)]
        [ValidateNever]
        public int role { get; set; }

        [Index(5)]
        [ValidateNever]
        public string status { get; set; }

        [Index(6)]
        [ValidateNever]
        public string remarks { get; set; }

        [Index(7)]
        [ValidateNever]
        public string created_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");

        [Index(8)]
        [ValidateNever]
        public string updated_at { get; set;} = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
    }
}
