
namespace Demo.Models
{
    public class ViewCompnaySession
    {
        public IEnumerable<Company> Companies { get; set; }

        public IEnumerable<Sessions> Session { get; set; }

        public IEnumerable<Department> Departments { get; set; }

        public IEnumerable<Sector> Sectors { get; set; }

        public Hiring_Departments Hiring_Departments { get; set; }

        public  Hiring_sectors Hiring_sectors { get; set; }

        public Hiring Hiring { get; set; }  
    }
}
