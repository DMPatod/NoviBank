using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using ECB.WebServer.Contracts.Wallets;
using Microsoft.AspNetCore.Mvc;
using NoviBank.Application.Wallets.Commands;
using NoviBank.Application.Wallets.Queries;

namespace ECB.WebServer.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class WalletsController : Controller
{
    private readonly IMessageHandler _messageHandler;

    public WalletsController(IMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetWallets()
    {
        var command = new GetAllWalletQuery();
        var result = await _messageHandler.SendAsync(command, CancellationToken.None);
        if (result.IsFailed)
        {
            throw new Exception();
        }

        return Ok(result.ValueOrDefault);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWallet([FromRoute] string id, [FromRoute] string currencyId = null!)
    {
        var command = new GetWalletQuery(id, currencyId);
        var result = await _messageHandler.SendAsync(command, CancellationToken.None);
        if (result.IsFailed)
        {
            throw new Exception();
        }

        return Ok(result.ValueOrDefault);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateWallet([FromRoute] string id, [FromBody] UpdateWalletRequest request)
    {
        var command = new UpdateWalletCommand(request.Strategy, id, request.Amount, request.CurrencyId);
        var result = await _messageHandler.SendAsync(command, CancellationToken.None);
        if (result.IsFailed)
        {
            throw new Exception();
        }

        return Ok(result.ValueOrDefault);
    }

    [HttpPost]
    public async Task<IActionResult> AddWallet([FromBody] AddWalletRequest request)
    {
        var command = new AddWalletCommand(request.CurrencyId);
        var result = await _messageHandler.SendAsync(command, CancellationToken.None);
        if (result.IsFailed)
        {
            throw new Exception();
        }

        return Ok(result.ValueOrDefault);
    }
}