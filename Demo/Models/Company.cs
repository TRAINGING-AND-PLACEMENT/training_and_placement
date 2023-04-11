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
        [JsonConverter(typeof(ParseStringConverter))]
        public long id { get; set; }
        
        [DisplayName("Company Identification Number")]
        [Required(ErrorMessage = "You must provide a identification number!")]
        [JsonProperty("cif")]
        public string cif { get; set; }

        [Required(ErrorMessage = "You must provide a name!")]
        [JsonProperty("name")]
        public string name { get; set; }

        [Required(ErrorMessage = "You must provide a contact number!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid contact number")]
        [JsonProperty("contact")]
        public string contact { get; set; }

        [Required(ErrorMessage = "You must provide a contact number!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [JsonProperty("alt_contact")]
        public string alt_contact { get; set; }

        [Required(ErrorMessage = "You must provide a emailaddress!")]
        [DataType(DataType.EmailAddress)]
        [JsonProperty("email")]
        public string email { get; set; }

        [Required(ErrorMessage = "You must provide a emailaddress!")]
        [JsonProperty("alt_email")]
        public string alt_email { get; set; }

        [Required]
        [JsonProperty("about")]
        public string about { get; set; }

        [ValidateNever]
        [JsonProperty("status")]
        [JsonConverter(typeof(ParseStringConverter))]
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
    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
