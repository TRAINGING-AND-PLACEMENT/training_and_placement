using Microsoft.AspNetCore.Http;

namespace Demo.api
{
    public class Webapi
    {
        HttpClient client;
        HttpResponseMessage response;
        Uri baseAddress = new Uri("http://169.254.223.131//api/api_modify.php?what=");

        public System.Uri api()
        {   
            return baseAddress;
        }

        public HttpResponseMessage responses(StringContent content)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            response = client.PostAsync(client.BaseAddress + "getlogin", content).Result;
            return response;
        }






        // Uri baseAddress = new Uri("https://tnpdeveloper.000webhostapp.com/api/api_modify.php?what=");

    }
}
