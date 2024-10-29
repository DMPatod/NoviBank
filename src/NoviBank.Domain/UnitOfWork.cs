using NoviBank.Domain.Currencies.Interfaces;
using NoviBank.Domain.Wallets.Interfaces;

namespace NoviBank.Domain;

public class UnitOfWork
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IWalletRepository _walletRepository;

    public UnitOfWork(IWalletRepository walletRepository, ICurrencyRepository currencyRepository)
    {
        _walletRepository = walletRepository;
        _currencyRepository = currencyRepository;
    }

    public ICurrencyRepository CurrencyRepository => _currencyRepository;
    public IWalletRepository WalletRepository => _walletRepository;
}