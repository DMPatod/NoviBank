using System.Data;
using DDD.Core.DomainObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoviBank.Domain.Currencies;
using NoviBank.Domain.Currencies.Interfaces;

namespace ECB.Infrastructure.DataPersistence.SqlServer.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly SqlServerContext _context;
    private readonly ILogger<CurrencyRepository> _logger;

    public CurrencyRepository(SqlServerContext context, ILogger<CurrencyRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<Currency?> FindAsync(DefaultGuidId id, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Currency>> FindAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<int> FindPaginatedAsync(int page, int pageSize,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<Currency> AddAsync(Currency entity, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Currency entity, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Currency entity, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task MergeRangeAsync(IEnumerable<(string, decimal)> pairs, DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var persistedEntities = await _context.Set<Currency>().Where(c => pairs.Select(p => p.Item1).Contains(c.Name))
            .ToDictionaryAsync(c => c.Name, cancellationToken);
        foreach (var item in pairs)
        {
            if (persistedEntities.TryGetValue(item.Item1, out var entity))
            {
                entity.Rate = item.Item2;
                _context.Update(entity);
            }
            else
            {
                _context.Add(Currency.Create(item.Item1, item.Item2));
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}