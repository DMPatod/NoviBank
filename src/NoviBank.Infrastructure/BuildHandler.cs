using ECB.Infrastructure.DataPersistence;
using ECB.Infrastructure.DataPersistence.SqlServer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoviBank.Domain;
using NoviBank.Domain.Currencies.Interfaces;
using NoviBank.Domain.Wallets.Interfaces;

namespace ECB.Infrastructure;

public static class BuildHandler
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataPersistence(configuration);

        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<UnitOfWork>();

        return services;
    }
}