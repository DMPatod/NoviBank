using ECB.ApiClient.Api;

namespace ECB.Client;

public class ApiTests
{
    [Fact]
    public async Task Test1()
    {
        var api = new RatesApi();
        var response = await api.ApiCurrencyGetAsync();

        Assert.NotEmpty(response);
        Assert.Contains(response, i => i.Currency == "USD");
    }
}