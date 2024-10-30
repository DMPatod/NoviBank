using FluentResults;
using NoviBank.Domain;
using NoviBank.Domain.Currencies;
using NoviBank.Domain.Wallets;
using NoviBank.Domain.Wallets.Interfaces;

namespace NoviBank.Application.Wallets.Services;

public class ForceSubtractFundsStrategy : IStrategy
{
    private readonly UnitOfWork _unitOfWork;

    public ForceSubtractFundsStrategy(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Wallet>> ExecuteAsync(Wallet wallet, decimal amount, Currency? currency = null,
        CancellationToken cancellationToken = default)
    {
        if (currency is null)
        {
            wallet.Balance -= amount;
        }
        else
        {
            wallet.Balance -= amount * (currency.Rate / wallet.Currency.Rate);
        }

        await _unitOfWork.WalletRepository.UpdateAsync(wallet, cancellationToken);
        return wallet;
    }
}