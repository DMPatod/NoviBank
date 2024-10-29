using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using ECB.ApiClient.Api;
using Microsoft.Extensions.Logging;
using NoviBank.Application.Currencies.Commands;
using Quartz;

namespace NoviBank.Application.Currencies;

public class EcbCurrencyUpdater : IJob
{
    private readonly ILogger<EcbCurrencyUpdater> _logger;
    private readonly IMessageHandler _messageHandler;

    public EcbCurrencyUpdater(ILogger<EcbCurrencyUpdater> logger, IMessageHandler messageHandler)
    {
        _logger = logger;
        _messageHandler = messageHandler;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.Log(LogLevel.Information, "EcbCurrencyUpdater is running.");

        var ecbExchangesClient = new RatesApi();
        var ecbExchanges = await ecbExchangesClient.ApiCurrencyGetAsync();

        _logger.Log(LogLevel.Information, $"Fetched {ecbExchanges.Count} exchanges.");

        var command = new UpdateCurrenciesFromEcbCommand(ecbExchanges);
        var result = await _messageHandler.SendAsync(command, default);
        if (result.IsFailed)
        {
            _logger.Log(LogLevel.Error, "Failed to update currencies.");
        }

        _logger.Log(LogLevel.Information, $"EcbCurrencyUpdater has ended.");
    }
}