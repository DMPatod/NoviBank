using DDD.Core.DomainObjects;
using DDD.Core.Repositories;

namespace NoviBank.Domain.Currencies.Interfaces;

public interface ICurrencyRepository : IBaseRepository<Currency, DefaultGuidId>
{
    Task UpdateRangeAsync(IList<(string, decimal)> pairs, DateOnly date, CancellationToken cancellationToken = default);
}