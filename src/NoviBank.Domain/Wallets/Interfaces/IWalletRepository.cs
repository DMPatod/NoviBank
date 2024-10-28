using DDD.Core.DomainObjects;
using DDD.Core.Repositories;

namespace NoviBank.Domain.Wallets.Interfaces;

public interface IWalletRepository : IBaseRepository<Wallet, DefaultGuidId>
{
}