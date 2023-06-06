using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class UploadCsv
    {
        [Required(ErrorMessage = "Please select a csv file.")]
        [DataType(DataType.Upload)]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.csv)$", ErrorMessage = "Only csv Files Allowed")]
        public IFormFile CSV_File { get; set; }


        [AllowNull]
        [ValidateNever]
        public IEnumerable<Department> Departments { get; set; }

        [DisplayName("Department Name")]
        [Required(ErrorMessage = "{0} is Required.")]
        [JsonProperty("department_id")]
        public int department_id { get; set; }
    }
}
