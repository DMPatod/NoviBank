using DDD.Core.DomainObjects;
using DDD.Core.Handlers;
using DDD.Core.Messages;
using FluentResults;
using NoviBank.Domain;
using NoviBank.Domain.Currencies;
using NoviBank.Domain.Wallets;

namespace NoviBank.Application.Wallets.Queries;

public record GetWalletQuery(string Id, string CurrencyId = null!) : IResultCommand<object>;

public class GetWalletQueryHandler : IResultComandHandler<GetWalletQuery, object>
{
    private readonly UnitOfWork _unitOfWork;

    public GetWalletQueryHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task<Wallet> GetWallet(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guid))
        {
            return null!;
        }

        var result = await _unitOfWork.WalletRepository.FindAsync(DefaultGuidId.Create(guid), cancellationToken);
        return result!;
    }

    private async Task<Currency> GetCurrency(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guid))
        {
            return null!;
        }

        var result = await _unitOfWork.CurrencyRepository.FindAsync(DefaultGuidId.Create(guid), cancellationToken);
        return result!;
    }

    public async Task<Result<object>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        var walletTask = GetWallet(request.Id, cancellationToken);
        var currencyTask = GetCurrency(request.CurrencyId, cancellationToken);
        await Task.WhenAll(walletTask, currencyTask);

        var wallet = await walletTask;
        var currency = await currencyTask;

        return new
        {
            Id = wallet.Id,
            Balance = wallet.Balance * (wallet.Currency.Rate / currency.Rate)
        };
    }
}