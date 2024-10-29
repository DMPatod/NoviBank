using DDD.Core.DomainObjects;
using NoviBank.Domain.Wallets;
using NoviBank.Domain.Wallets.Interfaces;

namespace ECB.Infrastructure.DataPersistence.SqlServer.Repositories;

public class WalletRepository : IWalletRepository
{
    public Task<Wallet?> FindAsync(DefaultGuidId id, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Wallet>> FindAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<int> FindPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<Wallet> AddAsync(Wallet entity, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Wallet entity, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Wallet entity, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}