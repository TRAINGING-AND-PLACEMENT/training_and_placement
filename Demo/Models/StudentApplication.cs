using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace Demo.Models
{
    public class StudentApplication
    {
        [JsonProperty("hiring_id")]
        public int hiring_id { get; set; }

        [JsonProperty("student_id")]
        public long student_id { get; set; }

        [JsonProperty("stipend")]
        public string stipend { get; set; }

        [JsonProperty("salary")]
        public string salary { get; set; }

        [ValidateNever]
        public int status { get; set; }

        [ValidateNever]
        public string remarks { get; set; }
    }
}
