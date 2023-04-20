namespace Demo.api
{
    public class Webapi
    {
        Uri baseAddress = new Uri("http://192.168.150.1/api/api_modify.php?what=");
        public System.Uri api()
        {
            return baseAddress;
        }
    }
}
