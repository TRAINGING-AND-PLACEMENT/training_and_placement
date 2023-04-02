using Demo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;


namespace Demo.Controllers.Json
{
    public class CompanyJson
    {
        public List<Company> listroot(List<Company> model, String data)
        {
            model = new List<Company>();
            RootObject root = JsonConvert.DeserializeObject<RootObject>(data);
            model = root.result;
            return model;
        }
        public Company uniroot(Company model, String data)
        {
            Root root = JsonConvert.DeserializeObject<Root>(data);
            model = root.result;
            return model;
        }
    }
    public class RootObject
    {
        public List<Company> result { get; set; }
    }
    public class Root
    {
        public Company result { get; set; }
    }
}