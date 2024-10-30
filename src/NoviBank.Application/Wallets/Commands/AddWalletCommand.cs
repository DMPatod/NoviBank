using DDD.Core.DomainObjects;
using DDD.Core.Handlers;
using DDD.Core.Messages;
using FluentResults;
using NoviBank.Domain;
using NoviBank.Domain.Wallets;

namespace NoviBank.Application.Wallets.Commands;

public record AddWalletCommand(string CurrencyId, decimal InitialBalance = 0) : IResultCommand<Wallet>;

public class AddWalletCommandHandler : IResultComandHandler<AddWalletCommand, Wallet>
{
    private readonly UnitOfWork _unitOfWork;

    public AddWalletCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Wallet>> Handle(AddWalletCommand request, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(request.CurrencyId, out var currencyId))
        {
            return Result.Fail("Currency Id Format is invalid");
        }

        var currency =
            await _unitOfWork.CurrencyRepository.FindAsync(DefaultGuidId.Create(currencyId), cancellationToken);
        if (currency is null)
        {
            return Result.Fail("Currency not found");
        }

        var wallet = Wallet.Create(currency, request.InitialBalance);
        await _unitOfWork.WalletRepository.AddAsync(wallet, cancellationToken);
        return wallet;
    }
}