using DDD.Core.Handlers;
using DDD.Core.Messages;
using ECB.ApiClient.Models;
using FluentResults;
using NoviBank.Domain;

namespace NoviBank.Application.Currencies.Commands;

public record UpdateCurrenciesFromEcbCommand(IList<Exchange> Exchanges) : IResultCommand;

public class UpdateCurrenciesFromEcbCommandHandler : IResultCommandHandler<UpdateCurrenciesFromEcbCommand>
{
    private readonly UnitOfWork _unitOfWork;

    public UpdateCurrenciesFromEcbCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateCurrenciesFromEcbCommand request, CancellationToken cancellationToken)
    {
        var pairs = request.Exchanges.Select(exchange => (exchange.Currency, exchange.Rate)).ToList();

        await _unitOfWork.CurrencyRepository.UpdateRangeAsync(pairs, request.Exchanges[0].Time, cancellationToken);

        return true;
    }
}