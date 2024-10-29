using DDD.Core.Handlers;
using DDD.Core.Messages;
using FluentResults;
using NoviBank.Domain.Wallets;

namespace NoviBank.Application.Wallets.Queries;

public record GetAllWalletQuery() : IResultCommand<IList<Wallet>>;

public class GetAllWalletQueryHandler : IResultComandHandler<GetAllWalletQuery, IList<Wallet>>
{
    public Task<Result<IList<Wallet>>> Handle(GetAllWalletQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}