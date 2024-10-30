using DDD.Core.Handlers;
using DDD.Core.Messages;
using FluentResults;

namespace NoviBank.Application.Wallets.Commands;

public record UpdateWalletCommand(decimal Amount, string Currency, string Strategy) : IResultCommand<object>;

public class UpdateWalletCommandHandler : IResultComandHandler<UpdateWalletCommand, object>
{
    public Task<Result<object>> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}