using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class User
    {
        [Key]
        [Index(0)]
        public int id { get; set; }
        [Index(1)]
        public string name { get; set; }
        [Index(2)]
        public string email { get; set; }
        [Index(3)]
        public string password { get; set; }
        [Index(4)]
        public int role { get; set; }
        [Index(5)]
        public string status { get; set; }
        [Index(6)]
        public string remarks { get; set; }
        [Index(7)]
        public string created_at { get; set; }
        [Index(8)]
        public string updated_at { get; set;}
    }
}
