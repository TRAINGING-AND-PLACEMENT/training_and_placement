
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class Department
    {
        [Key]
        [ValidateNever]
        public int id { get; set; }

        [Required]
        public string department { get; set; }

        [ValidateNever]
        public int status { get; set; }

        [ValidateNever]
        [AllowNull]
        public string remarks { get; set; }

        [ValidateNever]
        public string created_at { get; set; }

        [ValidateNever]
        public string updated_at { get; set; }
    }
}
