using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Demo.Models
{
    public class StudentApplication
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("hiring_id")]
        public int hiring_id { get; set; }

        [JsonProperty("student_id")]
        public long student_id { get; set; }

        [JsonProperty("min_stipend")]
        public string min_stipend { get; set; }

        [JsonProperty("max_stipend")]
        public string max_stipend { get; set; }

        [JsonProperty("min_salary")]
        public string min_salary { get; set; }

        [JsonProperty("max_salary")]
        public string max_salary { get; set; }

        [JsonProperty("date_of_joining")]
        public string date_of_joining { get; set; }

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
