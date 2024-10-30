using DDD.Core.Handlers;
using DDD.Core.Messages;
using ECB.ApiClient.Models;
using FluentResults;
using NoviBank.Domain;

namespace NoviBank.Application.Currencies.Commands;

public record RangeAddCurrencyCommand(IList<CurrencyItem> Currencies, DateOnly Date) : IResultCommand;

public record CurrencyItem(string Name, decimal Rate);

public class RangeAddCurrencyCommandHandler : IResultCommandHandler<RangeAddCurrencyCommand>
{
    private readonly UnitOfWork _unitOfWork;

    public RangeAddCurrencyCommandHandler(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(RangeAddCurrencyCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.CurrencyRepository.MergeRangeAsync(request.Currencies.Select(c => (c.Name, c.Rate)),
            request.Date,
            cancellationToken);

        return true;
    }
}