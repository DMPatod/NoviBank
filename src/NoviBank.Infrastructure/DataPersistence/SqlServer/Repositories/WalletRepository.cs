using DDD.Core.DomainObjects;
using NoviBank.Domain.Wallets;
using NoviBank.Domain.Wallets.Interfaces;

namespace ECB.Infrastructure.DataPersistence.SqlServer.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly SqlServerContext _context;

    public WalletRepository(SqlServerContext context)
    {
        _context = context;
    }

    public Task<Wallet?> FindAsync(DefaultGuidId id, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Wallet>> FindAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<int> FindPaginatedAsync(int page, int pageSize,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task<Wallet> AddAsync(Wallet entity, CancellationToken cancellationToken = new CancellationToken())
    {
        var ct = await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return ct.Entity;
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