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

    public async Task UpdateRangeAsync(IList<(string, decimal)> pairs, DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var paramsTable = new DataTable();
        
        paramsTable.Columns.Add("Id", typeof(Guid));
        paramsTable.Columns.Add("Name", typeof(string));
        paramsTable.Columns.Add("Rate", typeof(decimal));
        paramsTable.Columns.Add("Date", typeof(DateTime));

        foreach (var item in pairs)
        {
            paramsTable.Rows.Add(DefaultGuidId.Create(), item.Item1, item.Item2, date.ToDateTime(TimeOnly.MinValue));
        }

        await using var command = _context.Database.GetDbConnection().CreateCommand();
        
        command.CommandText = "dbo.CurrencyUpdateRange";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Insert(0, paramsTable);
        
        var param = command.CreateParameter();
        
        param.ParameterName = "@Currencies";
        param.DbType = DbType.Object;
        param.Value = paramsTable;
        
        command.Parameters.Add(param);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}