using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Hiring_Departments
    {
        [Key]
        [ValidateNever]
        public int id { get; set; }

        [ValidateNever]
        public long hiring_id { get; set; }

        [ValidateNever]
        public long department_id { get; set; }

        [ValidateNever]
        public int status { get; set; }

        [ValidateNever]
        public string remarks { get; set; }

        [ValidateNever]
        public string created_at { get; set; }

        [ValidateNever]
        public string updated_at { get; set; }
    }
}
