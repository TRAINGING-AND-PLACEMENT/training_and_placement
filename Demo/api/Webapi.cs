namespace Demo.api
{
    public class Webapi
    {
        Uri baseAddress = new Uri("http://192.168.1.103/api/apidemo?what=");
        public System.Uri api()
        {
            return baseAddress;
        }
    }
}
