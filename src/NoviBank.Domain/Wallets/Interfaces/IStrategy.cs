using FluentResults;
using NoviBank.Domain.Currencies;

namespace NoviBank.Domain.Wallets.Interfaces;

public interface IStrategy
{
    Task<Result<Wallet>> ExecuteAsync(Wallet wallet, decimal amount, Currency? currency = null,
        CancellationToken cancellationToken = default);
}