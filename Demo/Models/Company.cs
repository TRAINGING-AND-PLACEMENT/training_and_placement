namespace Demo.Models
{
    public class Company
    {
        
        public int id { get; set; }
        
        public string cif { get; set; }

        public string name { get; set; }

        public string about { get; set; }

        public string contact { get; set; }

        public string alt_contact { get; set; }

        public string email { get; set; }

        public string alt_email { get; set; }

        public int status { get; set; }

        public string remarks { get; set; }

        public string created_at { get; set; }

        public string updated_at { get; set;}
    }
}
