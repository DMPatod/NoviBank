using Moq;
using NoviBank.Application.Currencies.Commands;
using NoviBank.Domain;
using NoviBank.Domain.Currencies.Interfaces;

namespace NoviBank.Application.Tests.Currencies.Commands;

public class RangeAddCurrencyCommandTests
{
    [Fact]
    public async Task dwa()
    {
        var command = new RangeAddCurrencyCommand([
            new("USD", 1.0825M),
            new("JPY", 164.45M),
            new("BGN", 1.9558M),
            new("GBP", 0.83358M)
        ], DateOnly.Parse("2024-10-25"));

        var repo = new Mock<ICurrencyRepository>();

        var tList = command.Currencies.Select(c => (c.Name, c.Rate));
        repo.Setup(r => r.MergeRangeAsync(tList, command.Date, default));
        var unitOfWork = new UnitOfWork(null!, repo.Object);


        var handler = new RangeAddCurrencyCommandHandler(unitOfWork);
        var result = await handler.Handle(command, default);

        Assert.True(result.IsSuccess);

        repo.Verify(r => r.MergeRangeAsync(tList, command.Date, default), Times.Once);
    }
}