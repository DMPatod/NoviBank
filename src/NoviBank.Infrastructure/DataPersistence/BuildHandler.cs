using ECB.Infrastructure.DataPersistence.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECB.Infrastructure.DataPersistence;

public static class BuildHandler
{
    public static IServiceCollection AddDataPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlServerContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        return services;
    }
}