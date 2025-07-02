using Fanex.Caching;
using Fanex.Caching.Redis;
using Fanex.Data;
using Fanex.Data.Repository;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.Settlement.Converter.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
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

    public static void AddControllerAuthorization(this IServiceCollection services)
    {
        services.AddControllers(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }
        );
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

    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = configuration.GetSection("AuthSettings:LoginPath").Value;
                        options.LogoutPath = configuration.GetSection("AuthSettings:LogoutPath").Value;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("AuthSettings:CookieTimeOut").Value));
                        options.Cookie = new CookieBuilder
                        {
                            Name = "Fanex.Settlement-Support-Testing.Cookie",
                            SecurePolicy = CookieSecurePolicy.SameAsRequest
                        };
                        options.Events.OnSigningOut = context =>
                        {
                            context.Response.Redirect(options.LoginPath);
                            return Task.CompletedTask;
                        };
                    });
    }
}