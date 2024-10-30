using System.Net;
using System.Net.Http.Json;
using ECB.WebServer.Contracts.Wallets;
using ECB.WebServer.Controllers.V1;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NoviBank.WebServer.Tests.Controllers;

public class WalletsControllerTests
{
    private readonly WebApplicationFactory<WalletsController> _application = new();

    [Fact]
    public async Task Test1()
    {
        var client = _application.CreateClient();
        var response = await client.PostAsJsonAsync($"/v1/Wallets", new AddWalletRequest(""));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWallets_ReturnsOk()
    {
        var client = _application.CreateClient();
        var response = await client.GetAsync("/v1/Wallets");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWallet_ReturnsOk()
    {
        var client = _application.CreateClient();
        var response = await client.GetAsync("/v1/Wallets/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdateWallet_ReturnsOk()
    {
        var client = _application.CreateClient();
        var response = await client.PostAsJsonAsync("/v1/Wallets/1", new UpdateWalletRequest(100, "USD", "Strategy1"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddWallet_ReturnsOk()
    {
        var client = _application.CreateClient();
        var response = await client.PostAsJsonAsync("/v1/Wallets", new AddWalletRequest("USD"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
