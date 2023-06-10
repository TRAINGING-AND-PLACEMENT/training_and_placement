using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class Insert_User
    {
        [Key]
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConvertor))]
        public long id { get; set; }

        [DisplayName("Department Name")]
        [Required(ErrorMessage = "{0} is Required.")]
        [JsonProperty("department_id")]
        public int department_id { get; set; }

        [DisplayName("Session Name")]
        [Required(ErrorMessage = "{0} is Required.")]
        [JsonProperty("session_id")]
        public int session_id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "You must provide a User Name!")]
        [JsonProperty("name")]
        public string name { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "You must provide a User Email!")]
        [JsonProperty("email")]
        public string email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "You must provide a User Password!")]
        [JsonProperty("password")]
        public string password { get; set; }

        [DisplayName("User Role")]
        [JsonProperty("role")]
        public string role { get; set; } = "1";

        [AllowNull]
        [ValidateNever]
        public IEnumerable<Department> Departments { get; set; }

        [AllowNull]
        [ValidateNever]
        public IEnumerable<Sessions> Sessions { get; set; }

    }
}
