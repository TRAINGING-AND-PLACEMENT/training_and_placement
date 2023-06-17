using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class Company
    {
        [Key]
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConvertor))]
        public long id { get; set; }

        [JsonProperty("counts")]
        [ValidateNever]
        [AllowNull]
        public int counts { get; set; }

        [DisplayName("Company Identification Number")]
        [Required(ErrorMessage = "You must provide a identification number!")]
        [JsonProperty("cif")]
        public string cif { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "You must provide a name!")]
        [JsonProperty("name")]
        public string name { get; set; }

        [DisplayName("Company Contact No.")]
        [Required(ErrorMessage = "You must provide a contact number!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid contact number")]
        [JsonProperty("contact")]
        public string contact { get; set; }

        [DisplayName("Company Alternate No.")]
        [ValidateNever]
        [JsonProperty("alt_contact")]
        public string alt_contact { get; set; }

        [DisplayName("Company Email Address")]
        [Required(ErrorMessage = "You must provide a emailaddress!")]
        [DataType(DataType.EmailAddress)]
        [JsonProperty("email")]
        public string email { get; set; }

        [DisplayName("Company Alternate Email Address")]
        [ValidateNever]
        [JsonProperty("alt_email")]
        public string alt_email { get; set; }

        [DisplayName("Company About")]
        [Required]
        [JsonProperty("about")]
        public string about { get; set; }

        [ValidateNever]
        [JsonProperty("status")]
        [JsonConverter(typeof(ParseStringConvertor))]
        public long status { get; set; } = 0;

        [AllowNull]
        [ValidateNever]
        [JsonProperty("remarks")]
        public string remarks { get; set; }

        [ValidateNever]
        [JsonProperty("created_at")]
        public string created_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");

        [ValidateNever]
        [JsonProperty("updated_at")]
        public string updated_at { get; set;} = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
    }
}
