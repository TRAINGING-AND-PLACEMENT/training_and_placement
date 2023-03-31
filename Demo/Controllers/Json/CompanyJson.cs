using Demo.Models;
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
        public Company uniroot(Company user, String data)
        {
            Root root = JsonConvert.DeserializeObject<Root>(data);
            user = root.result;
            return user;
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