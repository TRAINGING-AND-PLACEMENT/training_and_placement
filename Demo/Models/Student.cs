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
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters allowed in name")]
		public string surname { get; set; }

        [Index(5)]
        [Required(ErrorMessage = "You must provide a first_name!")]
        [JsonProperty("first_name")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters allowed in name")]
		public string first_name { get; set; }

        [Index(6)]
        [Required(ErrorMessage = "You must provide a last_name!")]
        [JsonProperty("last_name")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters allowed in name")]
		public string last_name { get; set; }

        [Index(7)]
        [Required(ErrorMessage = "You must provide a grand_father_name!")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters allowed in name")]
		[JsonProperty("grand_father_name")]
        public string grand_father_name { get; set; }

        [Index(8)]
        [JsonProperty("enrollment")]
        public string enrollment { get; set; }

        [Index(9)]
        [Required(ErrorMessage = "You must provide a gender!")]
        [JsonProperty("gender")]
        public string gender { get; set; }

        [Index(10)]
        [Required(ErrorMessage = "You must provide a contact number!")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid Indian phone number.")]
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
		[RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid pincode")]
		public string pincode { get; set; }

        [Index(21)]
        [Required(ErrorMessage = "You must provide a city!")]
        [JsonProperty("city")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters are allowed in city name")]

		public string city { get; set; }

        [Index(22)]
        [JsonProperty("state")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters are allowed in state name")]
		public string state { get; set; }

        [Index(23)]
        [JsonProperty("permanent_address")]
        public string permanent_address { get; set; }

        [Index(24)]
        [JsonProperty("permanent_pincode")]
		[RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid pincode")]
		public string permanent_pincode { get; set; }

        [Index(25)]
        [JsonProperty("permanent_city")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters are allowed in city name")]
		public string permanent_city { get; set; }

        [Index(26)]
        [JsonProperty("permanent_state")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters are allowed in state name")]
		public string permanent_state { get; set; }

        [Index(27)]
        [JsonProperty("blood_group")]
		[RegularExpression(@"^(A|B|AB|O)[+-]?$", ErrorMessage = "Please enter a valid bloog group name")]
		public string blood_group { get; set; }

        [Index(28)]
        [JsonProperty("adhaar")]
		[RegularExpression(@"^\d{12}$", ErrorMessage = "Please enter a valid Aadhaar card number")]
		[Required(ErrorMessage ="Please enter your 14 digit AADHAR number")]
        public string adhaar { get; set; }

        [Index(29)]
		[RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Please enter a valid PAN card (format of ABCTY1234D) number")]
		[JsonProperty("pan")]
        public string pan { get; set; }

        [Index(30)]
        [RegularExpression(@"^(([A-Z]{2}[0-9]{2})( )|([A-Z]{2}-[0-9]{2}))((19|20)[0-9][0-9])[0-9]{7}$", ErrorMessage = "Please enter a valid Driving License card (format of HR-0619850034761) number")] 
        [JsonProperty("driving")]

        public string driving { get; set; }

        [Index(31)]
        [JsonProperty("ten_school")]
        public string ten_school { get; set; }

        [Index(32)]
        [JsonProperty("ten_passyear")]
		[RegularExpression(@"^\d{4}$", ErrorMessage = "Passing year can only be of 4 digits")]

		public string ten_passyear { get; set; }

        [Index(33)]
        [JsonProperty("ten_schooladdress")]
        public string ten_schooladdress { get; set; }

        [Index(34)]
		[RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid pincode")]
		[JsonProperty("ten_schoolpincode")]
        public string ten_schoolpincode { get; set; }

        [Index(35)]
        [JsonProperty("ten_schoolcity")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters are allowed in city name")]

		public string ten_schoolcity { get; set; }

        [Index(36)]
        [JsonProperty("ten_board")]
        public string ten_board { get; set; }

        [Index(37)]
		[RegularExpression(@"^\d+$", ErrorMessage = "only integer values allowed in marks")]

		[JsonProperty("ten_score")]
        public string ten_score { get; set; }

        [Index(38)]
        [JsonProperty("ten_scoreoutof")]
		[RegularExpression(@"^\d+$", ErrorMessage = "only integer values allowed in marks")]

		public string ten_scoreoutof { get; set; }

        [Index(39)]
        [JsonProperty("ten_gapno")]
        public string ten_gapno { get; set; }

        [Index(40)]
        [JsonProperty("ten_gapyears")]
        public string ten_gapyears { get; set; }

        [Index(41)]
        [JsonProperty("ten_admissionquota")]
        public string ten_admissionquota { get; set; }

        [Index(42)]
        [JsonProperty("twelve_school")]
        public string twelve_school { get; set; }

        [Index(43)]
        [JsonProperty("twelve_passyear")]
		[RegularExpression(@"^\d{4}$", ErrorMessage = "Passing year can only be of 4 digits")]

		public string twelve_passyear { get; set; }

        [Index(44)]
        [JsonProperty("twelve_schooladdress")]
        public string twelve_schooladdress { get; set; }

        [Index(45)]
        [JsonProperty("twelve_schoolpincode")]
		[RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid pincode")]

		public string twelve_schoolpincode { get; set; }

        [Index(46)]
        [JsonProperty("twelve_schoolcity")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters allowed in city name")]

		public string twelve_schoolcity { get; set; }

        [Index(47)]
        [JsonProperty("twelve_board")]
        public string twelve_board { get; set; }

        [Index(48)]
        [JsonProperty("twelve_score")]
		[RegularExpression(@"^\d+$", ErrorMessage = "only integer values allowed in marks")]

		public string twelve_score { get; set; }

        [Index(49)]
        [JsonProperty("twelve_scoreoutof")]
		[RegularExpression(@"^\d+$", ErrorMessage = "only integer values allowed in marks")]

		public string twelve_scoreoutof { get; set; }

        [Index(50)]
        [JsonProperty("twelve_gapno")]
        public string twelve_gapno { get; set; }

        [Index(51)]
        [JsonProperty("twelve_gapyears")]
        public string twelve_gapyears { get; set; }

        [Index(52)]
        [JsonProperty("twelve_admissionquota")]
        public string twelve_admissionquota { get; set; }

        [Index(53)]
        [JsonProperty("twelve_specialization")]
        public string twelve_specialization { get; set; }

        [Index(54)]
        [JsonProperty("ug_degree")]
        public string ug_degree { get; set; }

        [Index(55)]
        [JsonProperty("ug_college")]
        public string ug_college { get; set; }

        [Index(56)]
        [JsonProperty("ug_passyear")]
		[RegularExpression(@"^\d{4}$", ErrorMessage = "Passing year can only be of 4 digits")]


		public string ug_passyear { get; set; }

        [Index(57)]
        [JsonProperty("ug_collegeaddress")]
        public string ug_collegeaddress { get; set; }

        [Index(58)]
        [JsonProperty("ug_collegecity")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters allowed in city name")]

		public string ug_collegecity { get; set; }

        [Index(59)]
        [JsonProperty("ug_collegepincode")]
		[RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid pincode")]

		public string ug_collegepincode { get; set; }

        [Index(60)]
        [JsonProperty("ug_university")]
        public string ug_university { get; set; }

        [Index(61)]
        [JsonProperty("ug_score")]
		[RegularExpression(@"^\d+$", ErrorMessage = "only integer values allowed in marks")]

		public string ug_score { get; set; }

        [Index(62)]
        [JsonProperty("ug_scoreoutof")]
		[RegularExpression(@"^\d+$", ErrorMessage = "only integer values allowed in marks")]

		public string ug_scoreoutof { get; set; }

        [Index(63)]
        [JsonProperty("ug_gapno")]
        public string ug_gapno { get; set; }

        [Index(64)]
        [JsonProperty("ug_gapyears")]
        public string ug_gapyears { get; set; }

        [Index(65)]
        [JsonProperty("ug_admissionquota")]
        public string ug_admissionquota { get; set; }

        [Index(66)]
        [JsonProperty("pg_degree")]
        public string pg_degree { get; set; }

        [Index(67)]
        [JsonProperty("pg_college")]
        public string pg_college { get; set; }

        [Index(68)]
        [JsonProperty("pg_passyear")]

		public string pg_passyear { get; set; }

        [Index(69)]
        [JsonProperty("pg_collegeaddress")]
        public string pg_collegeaddress { get; set; }

        [Index(70)]
        [JsonProperty("pg_collegecity")]
		[RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters allowed in city name")]

		public string pg_collegecity { get; set; }

        [Index(71)]
        [JsonProperty("pg_collegepincode")]
		[RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter a valid pincode")]

		public string pg_collegepincode { get; set; }

        [Index(72)]
        [JsonProperty("pg_university")]
        public string pg_university { get; set; }

        [Index(73)]
        [JsonProperty("pg_score")]
		[RegularExpression(@"^\d+$", ErrorMessage = "only integer values allowed in marks")]

		public string pg_score { get; set; }

        [Index(74)]
        [JsonProperty("pg_scoreoutof")]
		[RegularExpression(@"^\d+$", ErrorMessage = "only integer values allowed in marks")]

		public string pg_scoreoutof { get; set; }

        [Index(75)]
        [JsonProperty("pg_gapno")]
        public string pg_gapno { get; set; }

        [Index(76)]
        [JsonProperty("pg_gapyears")]
        public string pg_gapyears { get; set; }

        [Index(77)]
        [JsonProperty("pg_admissionquota")]
        public string pg_admissionquota { get; set; }

        [Index(78)]
        [JsonProperty("status")]
        public string status { get; set; }

        [Index(79)]
        [JsonProperty("remarks")]
        public string remarks { get; set; }

        [Index(80)]
        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [Index(81)]
        [JsonProperty("updated_at")]
        public string updated_at { get; set; }

    }
}

