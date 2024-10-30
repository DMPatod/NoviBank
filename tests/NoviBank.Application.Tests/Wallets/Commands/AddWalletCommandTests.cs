using DDD.Core.DomainObjects;
using Moq;
using NoviBank.Application.Wallets.Commands;
using NoviBank.Domain;
using NoviBank.Domain.Currencies;
using NoviBank.Domain.Wallets;

namespace NoviBank.Application.Tests.Wallets.Commands;

public class AddWalletCommandTests
{
    [Fact]
    public async Task Handle_ShouldReturnFail_WhenCurrencyIdIsInvalid()
    {
        var unitOfWork = new Mock<UnitOfWork>();
        var handler = new AddWalletCommandHandler(unitOfWork.Object);
        var result = await handler.Handle(new AddWalletCommand("invalid-guid"), default);

        Assert.True(result.IsFailed);
        Assert.Equal("Currency Id Format is invalid", result.Errors[0].Message);
    }

    [Fact]
    public async Task Handle_ShouldReturnFail_WhenCurrencyNotFound()
    {
        var unitOfWork = new Mock<UnitOfWork>();
        unitOfWork.Setup(u => u.CurrencyRepository.FindAsync(It.IsAny<DefaultGuidId>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync((Currency)null);

        var handler = new AddWalletCommandHandler(unitOfWork.Object);
        var result = await handler.Handle(new AddWalletCommand("00000000-0000-0000-0000-000000000000"), default);

        Assert.True(result.IsFailed);
        Assert.Equal("Currency not found", result.Errors[0].Message);
    }

    [Fact]
    public async Task Handle_ShouldReturnWallet_WhenCurrencyIsValid()
    {
        var currency = Currency.Create("USD", 1.0m);
        var unitOfWork = new Mock<UnitOfWork>();
        unitOfWork.Setup(u => u.CurrencyRepository.FindAsync(It.IsAny<DefaultGuidId>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(currency);
        unitOfWork.Setup(u => u.WalletRepository.AddAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()))
                  .Returns((Task<Wallet>)Task.CompletedTask);

        var handler = new AddWalletCommandHandler(unitOfWork.Object);
        var result = await handler.Handle(new AddWalletCommand("00000000-0000-0000-0000-000000000000", 100), default);

        Assert.True(result.IsSuccess);
        Assert.Equal(currency, result.Value.Currency);
        Assert.Equal(100, result.Value.Balance);
    }
}
