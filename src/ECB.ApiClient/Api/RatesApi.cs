using ECB.ApiClient.Client;
using ECB.ApiClient.Models;
using Newtonsoft.Json;

namespace ECB.ApiClient.Api;

public interface IRatesApi : IApiAccessor
{
    
}

public partial class RatesApi : IRatesApi
{
    public Configuration Configuration { get; set; }

    public RatesApi()
    {
        Configuration = new Configuration();
    }

    public string GetBasePath()
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Exchange>> ApiCurrencyGetAsync()
    {
        return await ApiCurrencyGetAsyncWithHttpInfo();
    }

    public async Task<IList<Exchange>> ApiCurrencyGetAsyncWithHttpInfo()
    {
        var path = "/stats/eurofxref/eurofxref-daily.xml";
        var client = await Configuration.HttpClient.GetAsync(path);
        client.EnsureSuccessStatusCode();
        var xmlResult = await client.Content.ReadAsStringAsync();
        if (xmlResult is null)
        {
            throw new HttpRequestException("Response was null.");
        }

        return XmlDeserializer.Parse(xmlResult);
    }
}