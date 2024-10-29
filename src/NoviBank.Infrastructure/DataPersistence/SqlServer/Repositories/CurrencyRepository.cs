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
    private readonly ILogger<CurrencyRepository> _logger;
    private readonly SqlServerContext _context;

    public CurrencyRepository(ILogger<CurrencyRepository> logger, SqlServerContext context)
    {
        _logger = logger;
        _context = context;
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

    private const string _tempTableSql = """
                                         CREATE TABLE #ECB_CURRENCY (
                                             Id NVARCHAR(36) NOT NULL,
                                             Name NVARCHAR(10) NOT NULL,
                                             Rate DECIMAL (18, 5) NOT NULL,
                                             Updated_At DateTime NOT NULL,
                                         );
                                         """;

    private const string _mergeSql = """
                                     MERGE INTO Currencies AS target
                                     USING #ECB_CURRENCY AS source
                                     ON target.Name = source.Name
                                     WHEN MATCHED THEN
                                      UPDATE SET target.Rate = source.Rate
                                     WHEN NOT MATCHED THEN
                                      INSERT (Id, Name, Rate) VALUES (source.Id, source.Name, source.Rate);
                                     """;

    private const string _dropSql = """
                                    DROP TABLE #ECB_CURRENCY;
                                    """;

    private DataTable GetDataTable(IList<(string, decimal)> pairs, DateOnly date)
    {
        var table = new DataTable();
        table.Columns.Add("Id", typeof(Guid));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Rate", typeof(decimal));
        table.Columns.Add("Updated_At", typeof(DateTime));

        foreach (var item in pairs)
        {
            table.Rows.Add(DefaultGuidId.Create(), item.Item1, item.Item2, date.ToDateTime(TimeOnly.MinValue));
        }

        return table;
    }

    public async Task UpdateRangeAsync(IList<(string, decimal)> pairs, DateOnly date,
        CancellationToken cancellationToken = default)
    {
        // await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        // await _context.Database.ExecuteSqlAsync(_tempTableSql, cancellationToken);
        //
        // using (var bulkCopy = new SqlBulkCopy(_context.Database.GetConnectionString()))
        // {
        //     bulkCopy.DestinationTableName = "#ECB_CURRENCY";
        //     await bulkCopy.WriteToServerAsync(GetDataTable(pairs, date), cancellationToken);
        // }
        //
        // await _context.Database.ExecuteSqlAsync(_mergeSql, cancellationToken);
        //
        // await transaction.CommitAsync(cancellationToken);
    }
}