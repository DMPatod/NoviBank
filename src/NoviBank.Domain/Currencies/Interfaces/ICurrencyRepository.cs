using DDD.Core.DomainObjects;
using DDD.Core.Repositories;

namespace NoviBank.Domain.Currencies.Interfaces;

public interface ICurrencyRepository : IBaseRepository<Currency, DefaultGuidId>
{
    Task MergeRangeAsync(IEnumerable<(string, decimal)> currencies, DateOnly date,
        CancellationToken cancellationToken = default);
}