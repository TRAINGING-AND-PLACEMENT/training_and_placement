using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Data.SqlClient.DataClassification;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Sessions
    {
        [Key]
        [ValidateNever]
        public int id { get; set; }
        [ValidateNever]
        public string start_date { get; set; }
        [ValidateNever]
        public string end_date { get; set; }
        [ValidateNever]
        public string label { get; set; }
        [ValidateNever]
        public int default_year { get; set; }
        [ValidateNever]
        public string status { get; set; }
        [ValidateNever]
        public string remarks { get; set; }
        [ValidateNever]
        public string created_at { get; set; }
        [ValidateNever]
        public string updated_at { get; set; }
    }
}
