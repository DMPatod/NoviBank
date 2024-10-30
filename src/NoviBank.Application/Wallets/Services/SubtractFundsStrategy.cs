using FluentResults;
using NoviBank.Domain;
using NoviBank.Domain.Currencies;
using NoviBank.Domain.Wallets;
using NoviBank.Domain.Wallets.Interfaces;

namespace NoviBank.Application.Wallets.Services;

public class SubtractFundsStrategy : IStrategy
{
    private readonly UnitOfWork _unitOfWork;

    public SubtractFundsStrategy(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Wallet>> ExecuteAsync(Wallet wallet, decimal amount, Currency? currency = null,
        CancellationToken cancellationToken = default)
    {
        var mAmount = amount;
        if (currency is not null)
        {
            mAmount = amount * (currency.Rate / wallet.Currency.Rate);
        }

        if (mAmount > wallet.Balance)
        {
            return Result.Fail("Not enough funds for this wallet");
        }

        wallet.Balance -= mAmount;
        await _unitOfWork.WalletRepository.UpdateAsync(wallet, cancellationToken);
        return wallet;
    }
}