using Microsoft.Extensions.Logging;
using Quartz;

namespace NoviBank.Application.Currencies;

public class EcbCurrencyUpdater : IJob
{
    private readonly ILogger<EcbCurrencyUpdater> _logger;

    public EcbCurrencyUpdater(ILogger<EcbCurrencyUpdater> logger)
    {
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.Log(LogLevel.Information, "EcbCurrencyUpdater is running.");

        await Task.CompletedTask;
    }
}