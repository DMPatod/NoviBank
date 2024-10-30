using DDD.Core.DomainObjects;
using Moq;
using NoviBank.Application.Wallets.Queries;
using NoviBank.Domain;
using NoviBank.Domain.Currencies;
using NoviBank.Domain.Currencies.Interfaces;
using NoviBank.Domain.Wallets;
using NoviBank.Domain.Wallets.Interfaces;

namespace NoviBank.Application.Tests.Wallets.Queries;

public class GetWalletQueryTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenWalletAndCurrencyExist()
    {
        var walletId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();

        var walletRepo = new Mock<IWalletRepository>();
        walletRepo.Setup(r => r.FindAsync(DefaultGuidId.Create(walletId), default))
            .ReturnsAsync(new Wallet(DefaultGuidId.Create(walletId),
                new Currency(DefaultGuidId.Create(currencyId), "USD", 1M, DateTime.Now), 1.0M));
        var currencyRepo = new Mock<ICurrencyRepository>();
        currencyRepo.Setup(r => r.FindAsync(DefaultGuidId.Create(currencyId), default))
            .ReturnsAsync(new Currency(DefaultGuidId.Create(currencyId), "USD", 1M, DateTime.Now));
        var unitOfWork = new UnitOfWork(walletRepo.Object, currencyRepo.Object);

        var command = new GetWalletQuery(walletId.ToString(), currencyId.ToString());
        var handler = new GetWalletQueryHandler(unitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenWalletDoesNotExist()
    {
        var walletId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();

        var walletRepo = new Mock<IWalletRepository>();
        walletRepo.Setup(r => r.FindAsync(DefaultGuidId.Create(walletId), default))
            .ReturnsAsync((Wallet)null!);
        var currencyRepo = new Mock<ICurrencyRepository>();
        currencyRepo.Setup(r => r.FindAsync(DefaultGuidId.Create(currencyId), default))
            .ReturnsAsync(new Currency(DefaultGuidId.Create(currencyId), "USD", 1M, DateTime.Now));
        var unitOfWork = new UnitOfWork(walletRepo.Object, currencyRepo.Object);

        var command = new GetWalletQuery(walletId.ToString(), currencyId.ToString());
        var handler = new GetWalletQueryHandler(unitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenCurrencyDoesNotExist()
    {
        var walletId = Guid.NewGuid();
        var currencyId = Guid.NewGuid();

        var walletRepo = new Mock<IWalletRepository>();
        walletRepo.Setup(r => r.FindAsync(DefaultGuidId.Create(walletId), default))
            .ReturnsAsync(new Wallet(DefaultGuidId.Create(walletId),
                new Currency(DefaultGuidId.Create(currencyId), "USD", 1M, DateTime.Now), 1.0M));
        var currencyRepo = new Mock<ICurrencyRepository>();
        currencyRepo.Setup(r => r.FindAsync(DefaultGuidId.Create(currencyId), default))
            .ReturnsAsync((Currency)null!);
        var unitOfWork = new UnitOfWork(walletRepo.Object, currencyRepo.Object);

        var command = new GetWalletQuery(walletId.ToString(), currencyId.ToString());
        var handler = new GetWalletQueryHandler(unitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }
}
