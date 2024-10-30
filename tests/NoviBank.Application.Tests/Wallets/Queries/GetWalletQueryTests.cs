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
    public async Task dwa()
    {
        var walletId = Guid.NewGuid();

        var walletRepo = new Mock<IWalletRepository>();
        walletRepo.Setup(r => r.FindAsync(DefaultGuidId.Create(walletId), default))
            .ReturnsAsync(new Wallet(DefaultGuidId.Create(walletId),
                new Currency(DefaultGuidId.Create(), "", 1M, DateTime.Now), 1.0M));
        var currencyRepo = new Mock<ICurrencyRepository>();
        var unitOfWork = new UnitOfWork(walletRepo.Object, currencyRepo.Object);

        var command = new GetWalletQuery(walletId.ToString());
        var handler = new GetWalletQueryHandler(unitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}