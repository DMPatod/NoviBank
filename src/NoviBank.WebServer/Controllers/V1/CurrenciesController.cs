using System.Net.Mime;
using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using ECB.WebServer.Contracts.Currencies;
using Microsoft.AspNetCore.Mvc;
using NoviBank.Application.Currencies.Commands;

namespace ECB.WebServer.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class CurrenciesController : ControllerBase
{
    private readonly IMessageHandler _messageHandler;

    public CurrenciesController(IMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    [HttpPost]
    public async Task<IActionResult> AddCurrenties([FromBody] AddCurrencyRequest request)
    {
        var command = new RangeAddCurrencyCommand(request.Currencies, DateOnly.Parse(request.Date));
        var result = await _messageHandler.SendAsync(command, CancellationToken.None);
        if (result.IsFailed)
        {
            throw new Exception();
        }

        return Ok(result.ValueOrDefault);
    }
}