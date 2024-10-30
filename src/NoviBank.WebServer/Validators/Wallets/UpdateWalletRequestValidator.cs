using ECB.WebServer.Contracts.Wallets;
using FluentValidation;
using NoviBank.Domain.Wallets.Enums;

namespace ECB.WebServer.Validators.Wallets;

public class UpdateWalletRequestValidator : AbstractValidator<UpdateWalletRequest>
{
    public UpdateWalletRequestValidator()
    {
        RuleFor(r => r.Strategy)
            .NotEmpty().WithMessage("Please specify a strategy")
            .Must(s => Enum.TryParse(typeof(StrategyType), s, true, out _))
            .WithMessage("Please specify a valid strategy");
    }
}