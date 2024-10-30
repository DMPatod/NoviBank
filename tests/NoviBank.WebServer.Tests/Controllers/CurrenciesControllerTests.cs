using System.Net;
using System.Net.Http.Json;
using ECB.WebServer.Contracts.Currencies;
using ECB.WebServer.Controllers.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using NoviBank.Application.Currencies.Commands;

namespace NoviBank.WebServer.Tests.Controllers;

public class CurrenciesControllerTests
{
    private readonly WebApplicationFactory<CurrenciesController> _application = new();

    [Fact]
    public async Task UpdateCurrencies()
    {
        var client = _application.CreateClient();
        var request = new AddCurrencyRequest(new List<CurrencyItem>
        {
            new("USD", 1.0825M),
            new("JPY", 164.45M),
            new("BGN", 1.9558M),
            new("GBP", 0.83358M)
        }, "2024-10-25");

        var response = await client.PostAsJsonAsync("api/v1/Currencies", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCurrencies_InvalidDate()
    {
        var client = _application.CreateClient();
        var request = new AddCurrencyRequest(new List<CurrencyItem>
        {
            new("USD", 1.0825M),
            new("JPY", 164.45M),
            new("BGN", 1.9558M),
            new("GBP", 0.83358M)
        }, "invalid-date");

        var response = await client.PostAsJsonAsync("api/v1/Currencies", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCurrencies_EmptyCurrencies()
    {
        var client = _application.CreateClient();
        var request = new AddCurrencyRequest(new List<CurrencyItem>(), "2024-10-25");

        var response = await client.PostAsJsonAsync("api/v1/Currencies", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCurrencies_NullCurrencies()
    {
        var client = _application.CreateClient();
        var request = new AddCurrencyRequest(null, "2024-10-25");

        var response = await client.PostAsJsonAsync("api/v1/Currencies", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
