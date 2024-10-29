using System.Reflection;
using DDD.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;
using NoviBank.Application.Currencies;
using Quartz;

namespace NoviBank.Application;

public static class BuildHandler
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddQuartz(c =>
        {
            var key = new JobKey("CurrencyUpdater");
            c.AddJob<EcbCurrencyUpdater>(c => c.WithIdentity(key));
            c.AddTrigger(c => c.ForJob(key).WithIdentity("CurrencyUpdater-Trigger").WithCronSchedule("0 * * ? * *"));
        });
        services.AddQuartzHostedService();

        services.AddDefaultMessageHandler(Assembly.GetExecutingAssembly());

        return services;
    }
}