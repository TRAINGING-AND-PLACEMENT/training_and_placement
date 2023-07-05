using Newtonsoft.Json;

namespace Demo.Models
{
    public class StudentReport
    {
        [JsonProperty("label")]
        public string label { get; set; }

        [JsonProperty("department")]
        public string department_name { get; set; }

        [JsonProperty("enrollment")]
        public string enrollment { get; set; }

        [JsonProperty("surname")]
        public string student_surname { get; set; }

        [JsonProperty("first_name")]
        public string student_firstname { get; set; }

        [JsonProperty("last_name")]
        public string student_lastname { get; set; }

        [JsonProperty("email")]
        public string student_email { get; set; }

        [JsonProperty("contact")]
        public string student_contact { get; set; }

        [JsonProperty("name")]
        public string company_name { get; set; }

        [JsonProperty("max_stipend")]
        public string max_stipend { get; set; }

        [JsonProperty("min_stipend")]
        public string min_stipend { get; set; }

        [JsonProperty("max_salary")]
        public string max_salary { get; set; }

        [JsonProperty("min_salary")]
        public string min_salary { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }
    }
}
