using Moq;
using NoviBank.Application.Currencies.Commands;
using NoviBank.Domain;
using NoviBank.Domain.Currencies.Interfaces;

namespace NoviBank.Application.Tests.Currencies.Commands;

public class RangeAddCurrencyCommandTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenMergeRangeAsyncSucceeds()
    {
        var command = new RangeAddCurrencyCommand(new List<CurrencyItem>
        {
            new("USD", 1.0825M),
            new("JPY", 164.45M),
            new("BGN", 1.9558M),
            new("GBP", 0.83358M)
        }, DateOnly.Parse("2024-10-25"));

        var repo = new Mock<ICurrencyRepository>();

        var tList = command.Currencies.Select(c => (c.Name, c.Rate));
        repo.Setup(r => r.MergeRangeAsync(tList, command.Date, default)).Returns(Task.CompletedTask);
        var unitOfWork = new UnitOfWork(null!, repo.Object);

        var handler = new RangeAddCurrencyCommandHandler(unitOfWork);
        var result = await handler.Handle(command, default);

        Assert.True(result.IsSuccess);

        repo.Verify(r => r.MergeRangeAsync(tList, command.Date, default), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenMergeRangeAsyncFails()
    {
        var command = new RangeAddCurrencyCommand(new List<CurrencyItem>
        {
            new("USD", 1.0825M),
            new("JPY", 164.45M),
            new("BGN", 1.9558M),
            new("GBP", 0.83358M)
        }, DateOnly.Parse("2024-10-25"));

        var repo = new Mock<ICurrencyRepository>();

        var tList = command.Currencies.Select(c => (c.Name, c.Rate));
        repo.Setup(r => r.MergeRangeAsync(tList, command.Date, default)).ThrowsAsync(new Exception("Database error"));
        var unitOfWork = new UnitOfWork(null!, repo.Object);

        var handler = new RangeAddCurrencyCommandHandler(unitOfWork);
        var result = await handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal("Database error", result.Errors.First().Message);

        repo.Verify(r => r.MergeRangeAsync(tList, command.Date, default), Times.Once);
    }
}
