namespace Demo.api
{
    public class Webapi
    {

        Uri baseAddress = new Uri("http://127.0.0.1/api/api_modify.php?what=");

       // Uri baseAddress = new Uri("https://tnpdeveloper.000webhostapp.com/api/api_modify.php?what=");

        public System.Uri api()
        {
            return baseAddress;
        }
    }
}
