using DDD.Core.DomainObjects;
using DDD.Core.Handlers;
using DDD.Core.Messages;
using FluentResults;
using NoviBank.Domain;
using NoviBank.Domain.Currencies;
using NoviBank.Domain.Wallets.Enums;

namespace NoviBank.Application.Wallets.Commands;

public record UpdateWalletCommand(string Strategy, string WalletId, decimal Amount, string? CurrencyId = null)
    : IResultCommand<object>;

public class UpdateWalletCommandHandler : IResultComandHandler<UpdateWalletCommand, object>
{
    private readonly UnitOfWork _unitOfWork;
    private readonly StrategyFactory _strategyFactory;

    public UpdateWalletCommandHandler(StrategyFactory strategyFactory, UnitOfWork unitOfWork)
    {
        _strategyFactory = strategyFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(request.WalletId, out var walletId))
        {
            return Result.Fail("Invalid wallet id");
        }

        var wallet = _unitOfWork.WalletRepository.FindAsync(DefaultGuidId.Create(walletId), cancellationToken).Result;
        if (wallet is null)
        {
            return Result.Fail("Invalid wallet passed");
        }

        Currency? currency = null;
        if (request.CurrencyId is not null)
        {
            if (Guid.TryParse(request.CurrencyId, out var currencyId))
            {
                return Result.Fail("Invalid currency id");
            }

            currency = await _unitOfWork.CurrencyRepository.FindAsync(DefaultGuidId.Create(currencyId),
                cancellationToken);
            if (currency is null)
            {
                return Result.Fail("Invalid currency passed");
            }
        }

        var strategyType = (StrategyType)Enum.Parse(typeof(StrategyType), request.Strategy);
        var strategy = _strategyFactory.GetStrategy(strategyType);

        return await strategy.ExecuteAsync(wallet, request.Amount, currency, cancellationToken);
    }
}