using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using ECB.ApiClient.Api;
using ECB.ApiClient.Models;
using FluentResults;
using Microsoft.Extensions.Logging;
using Moq;
using NoviBank.Application.Currencies;
using NoviBank.Application.Currencies.Commands;
using Quartz;

namespace NoviBank.Application.Tests.Currencies;

public class EcbCurrencyUpdaterTests
{
    [Fact]
    public async Task Execute_ShouldLogInformationAndSendCommand()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<EcbCurrencyUpdater>>();
        var messageHandlerMock = new Mock<IMessageHandler>();
        var ratesApiMock = new Mock<RatesApi>();
        var jobExecutionContextMock = new Mock<IJobExecutionContext>();

        var ecbExchanges = new List<Exchange>
                    {
                        new Exchange( "USD", "1.1", "2024-10-10" ),
                        new Exchange("EUR", "0.9", "2024-10-10")
                    };

        ratesApiMock.Setup(api => api.ApiCurrencyGetAsync()).ReturnsAsync(ecbExchanges);
        messageHandlerMock.Setup(mh => mh.SendAsync(It.IsAny<RangeAddCurrencyCommand>(), default))
                          .ReturnsAsync(Result.Ok());

        var updater = new EcbCurrencyUpdater(loggerMock.Object, messageHandlerMock.Object);

        // Act
        await updater.Execute(jobExecutionContextMock.Object);

        // Assert
        loggerMock.Verify(logger => logger.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("EcbCurrencyUpdater is running.")),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);

        loggerMock.Verify(logger => logger.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"Fetched {ecbExchanges.Count} exchanges.")),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);

        messageHandlerMock.Verify(mh => mh.SendAsync(It.IsAny<RangeAddCurrencyCommand>(), default), Times.Once);
    }
}
