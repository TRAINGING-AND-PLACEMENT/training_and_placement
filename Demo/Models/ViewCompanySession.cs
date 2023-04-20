
namespace Demo.Models
{
    public class ViewCompnaySession
    {
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Sessions> Session { get; set; }

        public Hiring Hiring { get; set; }
    }
}
