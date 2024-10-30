using Microsoft.Extensions.DependencyInjection;
using NoviBank.Application.Wallets.Services;
using NoviBank.Domain.Wallets.Enums;
using NoviBank.Domain.Wallets.Interfaces;

namespace NoviBank.Application.Wallets;

public class StrategyFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStrategy GetStrategy(StrategyType strategyTypeType)
    {
        return strategyTypeType switch
        {
            StrategyType.AddFunds => _serviceProvider.GetRequiredService<AddFundsWalletStrategy>(),
            StrategyType.SubtractFunds => _serviceProvider.GetRequiredService<SubtractFundsStrategy>(),
            StrategyType.ForceSubtractFunds => _serviceProvider.GetRequiredService<ForceSubtractFundsStrategy>(),
            _ => throw new ArgumentOutOfRangeException(nameof(strategyTypeType), strategyTypeType, null)
        };
    }
}