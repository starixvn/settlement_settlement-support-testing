using Fanex.Caching;
using Fanex.Caching.Redis;
using Fanex.Data;
using Fanex.Data.Repository;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.Settlement.Converter.Services;
using WebSite.Services;
using ILogger = Fanex.Logging.ILogger;

namespace WebSite;

public static class InitializeInjector
{
    public static void InitializeServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddFanexData(configuration);
        service.AddFanexLogSentry(configuration);
        service.AddTransient<ILogger, Logger>();
        service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        service.AddSingleton<XmlConvertService, XmlConvertServiceImp>();
        service.AddSingleton<IDynamicRepository, DynamicRepository>();
        service.AddSingleton<SoccerResultService, SoccerResultServiceImp>();
        service.AddSingleton<ProviderResultService, ProviderResultServiceImp>();
        service.AddSingleton<LeaguesTestService, LeaguesTestServiceImp>();
        service.AddSingleton<QueryService, QueryServiceImp>();

        service.AddConfigRedisCache(configuration);
    }

    public static void AddConfigRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Redis:ConnectionString");
        var applicationName = configuration.GetValue<string>("Redis:ApplicationName");

        services.AddSingleton<ICache>(new RedisCache(new RedisCacheOptions
        {
            ConnectionString = connectionString,
            ApplicationName = applicationName,
        }));
        services.AddSingleton<ICacheService>(new CacheService());
    }
}