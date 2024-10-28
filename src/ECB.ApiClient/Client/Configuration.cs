namespace ECB.ApiClient.Client;

public class Configuration
{
    public HttpClient HttpClient { get; set; }

    public Configuration()
    {
        HttpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://ecb.europa.eu/api/v2/")
        };
    }
}