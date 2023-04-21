namespace Demo.Models
{
    public class ViewHiring
    {
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Sessions> Session { get; set; }
        public IEnumerable<Hiring> Hirings { get; set; }
    }
}
