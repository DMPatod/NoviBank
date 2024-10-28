using DDD.Core.DomainObjects;
using NoviBank.Domain.Currencies;

namespace NoviBank.Domain.Wallets;

public class Wallet : AggregateRoot<DefaultGuidId>
{
    public Currency Currency { get; private set; }
    public decimal Balance { get; private set; }

    private Wallet()
    {
        // For EF Only.
    }

    internal Wallet(DefaultGuidId id, Currency currency, decimal balance)
        : base(id)
    {
        Currency = currency;
        Balance = balance;
    }

    public static Wallet Create(Currency currency, decimal balance)
    {
        return new Wallet(DefaultGuidId.Create(), currency, balance);
    }

    public void Add(decimal amount)
    {
        Balance += amount;
    }
}