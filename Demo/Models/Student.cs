using System.ComponentModel.DataAnnotations;
using static System.Formats.Asn1.AsnWriter;

namespace Demo.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public int session_id { get; set; }
        public int user_id { get; set; }
        public string surname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gname { get; set; }
        public string enrollment { get; set; }
        public string gender { get; set; }
        public string contact { get; set; }
        public string altcontact { get; set; }
        public string parentcontact { get; set; }
        public string dob { get; set; }
        public string langeng { get; set; }
        public string langhindi { get; set; }
        public string langguj { get; set; }
        public string langmarathi { get; set; }
        public string langother { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string permanentaddress { get; set; }
        public string permanentpincode { get; set; }
        public string permanentcity { get; set; }
        public string permanentstate { get; set; }
        public string bloodgroup { get; set; }
        public string adhaar { get; set; }
        public string pan { get; set; }
        public string driving { get; set; }
        public string 10school { get; set; }
        public string 10passyear { get; set; }
        public string 10schooladdress { get; set; }
        public string 10schoolpincode { get; set; }
        public string 10schoolcity { get; set; }
        public string 10board { get; set; }
        public string 10score { get; set; }
        public string 10scoreoutof { get; set; }
        public string 10gapno { get; set; }
        public string 10gapyears { get; set; }
        public string 10admissionquota { get; set; }
        public string 12school { get; set; }
        public string 12passyear { get; set; }
        public string 12schooladdress { get; set; }
        public string 12schoolpincode { get; set; }
        public string 12schoolcity { get; set; }
        public string 12board { get; set; }
        public string 12score { get; set; }
        public string 12scoreoutof { get; set; }
        public string 12gapno { get; set; }
        public string 12gapyears { get; set; }  
        public string 12admissionquota { get; set; }
        public string 12specialization { get; set; }
        public string ugdegree { get; set; }
        public string ugcollege { get; set; }
        public string ugpassyear { get; set; }
        public string ugcollegeaddress { get; set; }
        public string ugcollegecity { get; set; }
        public string ugcollegepincode { get; set; }
        public string ugdepartmentid { get; set; }
        public string ugsectorid { get; set; }
        public string uguniversity { get; set; }
        public string ugscore { get; set; }
        public string ugscoreoutof { get; set; }
        public string uggapno { get; set; }
        public string uggapyears { get; set; }
        public string ugadmissionqouta { get; set; }
        public string pgdegree { get; set; }
        public string pgcollege { get; set; }
        public string pgpassyear { get; set; }
        public string pgcollegeaddress { get; set; }
        public string pgcollegecity { get; set; }
        public string pgcollegepincode { get; set; }
        public string pgdepartmentid { get; set; }
        public string pgsectorid { get; set; }
        public string pguniversity { get; set; }
        public string pgscore { get; set; }
        public string pgscoreoutof { get; set; }
        public string pggapno { get; set; }
        public string pggapyears { get; set; }
        public string pgadmissionqouta { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
