using DDD.Core.Handlers;
using DDD.Core.Messages;
using FluentResults;
using NoviBank.Domain.Wallets;

namespace NoviBank.Application.Wallets.Commands;

public record AddWalletCommand() : IResultCommand<Wallet>;

public class AddWalletCommandHandler : IResultComandHandler<AddWalletCommand, Wallet>
{
    public Task<Result<Wallet>> Handle(AddWalletCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}