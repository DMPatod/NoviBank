using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using Microsoft.AspNetCore.Mvc;

namespace ECB.WebServer.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class WalletsController : Controller
{
    private readonly IMessageHandler _messageHandler;
    private readonly ILogger<WalletsController> _logger;

    public WalletsController(IMessageHandler messageHandler, ILogger<WalletsController> logger)
    {
        _messageHandler = messageHandler;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetWallets()
    {
        return Ok();
    }
}