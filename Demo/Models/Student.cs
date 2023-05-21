using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class Student
    {
        [Key]
        [Index(0)]
        [JsonProperty("id")]
        public int id { get; set; }
        [Index(1)]
        [JsonProperty("session_id")]

        public int session_id { get; set; }
        [Index(2)]
        [JsonProperty("user_id")]

        public int user_id { get; set; }
        [Index(3)]
        [JsonProperty("department_id")]

        public int department_id { get; set; }
        [Index(4)]

        [Required(ErrorMessage = "You must provide a surname!")]
        [JsonProperty("surname")]
        public string surname { get; set; }
        [Index(5)]

        [Required(ErrorMessage = "You must provide a first_name!")]
        [JsonProperty("first_name")]
        public string first_name { get; set; }
        [Index(6)]

        [Required(ErrorMessage = "You must provide a last_name!")]
        [JsonProperty("last_name")]
        public string last_name { get; set; }
        [Index(7)]

        [Required(ErrorMessage = "You must provide a grand_father_name!")]
        [JsonProperty("grand_father_name")]
        public string grand_father_name { get; set; }
        [Index(8)]

        [Required(ErrorMessage = "You must provide your Enrollment number!")]
        [JsonProperty("enrollment")]
        public string enrollment { get; set; }
        [Index(9)]

        [Required(ErrorMessage = "You must provide a gender!")]
        [JsonProperty("gender")]
        public string gender { get; set; }
        [Index(10)]

        [Required(ErrorMessage = "You must provide a contact number!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid contact number")]
        [JsonProperty("contact")]
        public string contact { get; set; }
        [Index(11)]

        [Required(ErrorMessage = "You must provide a alt_contact number!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid contact number")]
        [JsonProperty("alt_contact")]
        public string alt_contact { get; set; }
        [Index(12)]

        [Required(ErrorMessage = "You must provide a parent_contact number!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid contact number")]
        [JsonProperty("parent_contact")]
        public string parent_contact { get; set; }
        [Index(13)]

        [Required(ErrorMessage = "Please Select Your Date Of Birth")]
        [JsonProperty("dob")]
        public string dob { get; set; }
        [Index(14)]
        [JsonProperty("lang_eng")]

        public string lang_eng { get; set; }
        [Index(15)]
        [JsonProperty("lang_hindi")]
        public string lang_hindi { get; set; }
        [Index(16)]
        [JsonProperty("lang_guj")]
        public string lang_guj { get; set; }
        [Index(17)]
        [JsonProperty("lang_marathi")]
        public string lang_marathi { get; set; }
        [Index(18)]
        [JsonProperty("lang_other")]
        public string lang_other { get; set; }
        [Index(19)]

        [Required(ErrorMessage = "You must provide a address!")]
        [JsonProperty("address")]
        public string address { get; set; }
        [Index(20)]

        [Required(ErrorMessage = "You must provide a pincode!")]
        [JsonProperty("pincode")]
        public string pincode { get; set; }
        [Index(21)]

        [Required(ErrorMessage = "You must provide a city!")]
        [JsonProperty("city")]
        public string city { get; set; }
        [Index(22)]
        [JsonProperty("state")]
        public string state { get; set; }
        [Index(23)]
        [JsonProperty("permanent_address")]

        public string permanent_address { get; set; }
        [Index(24)]
        [JsonProperty("permanent_pincode")]

        public string permanent_pincode { get; set; }
        [Index(25)]
        [JsonProperty("permanent_city")]

        public string permanent_city { get; set; }
        [Index(26)]
        [JsonProperty("permanent_state")]

        public string permanent_state { get; set; }
        [Index(27)]
        [JsonProperty("blood_group")]

        public string blood_group { get; set; }
        [Index(28)]
        [JsonProperty("adhaar")]

        public string adhaar { get; set; }
        [Index(29)]
        [JsonProperty("pan")]

        public string pan { get; set; }
        [Index(30)]
        [JsonProperty("driving")]

        public string driving { get; set; }
        [Index(31)]

        public string ten_school { get; set; }
        [Index(32)]
        public string ten_passyear { get; set; }
        [Index(33)]
        public string ten_schooladdress { get; set; }
        [Index(34)]
        public string ten_schoolpincode { get; set; }
        [Index(35)]
        public string ten_schoolcity { get; set; }
        [Index(36)]
        public string ten_board { get; set; }
        [Index(37)]
        public string ten_score { get; set; }
        [Index(38)]
        public string ten_scoreoutof { get; set; }
        [Index(39)]
        public string ten_gapno { get; set; }
        [Index(40)]
        public string ten_gapyears { get; set; }
        [Index(41)]
        public string ten_admissionquota { get; set; }
        [Index(42)]
        public string twelve_school { get; set; }
        [Index(43)]
        public string twelve_passyear { get; set; }
        [Index(44)]
        public string twelve_schooladdress { get; set; }
        [Index(45)]
        public string twelve_schoolpincode { get; set; }
        [Index(46)]
        public string twelve_schoolcity { get; set; }
        [Index(47)]
        public string twelve_board { get; set; }
        [Index(48)]
        public string twelve_score { get; set; }
        [Index(49)]
        public string twelve_scoreoutof { get; set; }
        [Index(50)]
        public string twelve_gapno { get; set; }
        [Index(51)]
        public string twelve_gapyears { get; set; }
        [Index(52)]
        public string twelve_admissionquota { get; set; }
        [Index(53)]
        public string twelve_specialization { get; set; }
        [Index(54)]
        public string ug_degree { get; set; }
        [Index(55)]
        public string ug_college { get; set; }
        [Index(56)]
        public string ug_passyear { get; set; }
        [Index(57)]
        public string ug_collegeaddress { get; set; }
        [Index(58)]
        public string ug_collegecity { get; set; }
        [Index(59)]
        public string ug_collegepincode { get; set; }
        [Index(60)]
        public string ug_university { get; set; }
        [Index(61)]
        public string ug_score { get; set; }
        [Index(62)]
        public string ug_scoreoutof { get; set; }
        [Index(63)]
        public string ug_gapno { get; set; }
        [Index(64)]
        public string ug_gapyears { get; set; }
        [Index(65)]
        public string ug_admissionquota { get; set; }
        [Index(66)]
        public string pg_degree { get; set; }
        [Index(67)]
        public string pg_college { get; set; }
        [Index(68)]
        public string pg_passyear { get; set; }
        [Index(69)]
        public string pg_collegeaddress { get; set; }
        [Index(70)]
        public string pg_collegecity { get; set; }
        [Index(71)]
        public string pg_collegepincode { get; set; }
        [Index(72)]
        public string pg_university { get; set; }
        [Index(73)]
        public string pg_score { get; set; }
        [Index(74)]
        public string pg_scoreoutof { get; set; }
        [Index(75)]
        public string pg_gapno { get; set; }
        [Index(76)]
        public string pg_gapyears { get; set; }
        [Index(77)]
        public string pg_admissionquota { get; set; }
        [Index(78)]
        public string status { get; set; }
        [Index(79)]
        public string remarks { get; set; }
        [Index(80)]
        public string created_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
        [Index(81)]
        public string updated_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
    }
}
