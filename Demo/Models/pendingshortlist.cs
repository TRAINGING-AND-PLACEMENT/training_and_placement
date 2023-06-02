using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class pendingshortlist
    {
        [JsonProperty("hiring_id")]
        public int hiring_id { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("session_id")]
        public int session_id { get; set; }

        [JsonProperty("user_id")]
        public int user_id { get; set; }

        [JsonProperty("department_id")]
        public int department_id { get; set; }

        [JsonProperty("surname")]
        public string surname { get; set; }

        [JsonProperty("first_name")]
        public string first_name { get; set; }

        [JsonProperty("last_name")]
        public string last_name { get; set; }

        [JsonProperty("enrollment")]
        public string enrollment { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("contact")]
        public string contact { get; set; }

        [JsonProperty("dob")]
        public string dob { get; set; }

        [JsonProperty("adhaar")]
        public string adhaar { get; set; }

        [JsonProperty("pan")]
        public string pan { get; set; }
    }
}
