using Fanex.Caching;
using Fanex.Caching.Redis;
using Fanex.Data;
using Fanex.Data.SqlServer;
using System.Configuration;
using System.Web.Http;

namespace WebSite
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            StartDataBaseSetting();

            StartRedisSetting();
        }

        private static void StartDataBaseSetting()
        {
            DbSettingProviderManager
              .StartNewSession()
              .WithSqlServer(ConfigurationManager.AppSettings["StoredProcedurePath"])
              .Run();
        }

        private static void StartRedisSetting()
        {
            CacheServiceManager.Use(() => new RedisCache(new RedisCacheOptions
            {
                ConnectionString = $"{ConfigurationManager.AppSettings["Redis.Server"]}:{ConfigurationManager.AppSettings["Redis.Port"]},password={ConfigurationManager.AppSettings["Redis.Password"]},defaultDatabase=11",
                ApplicationName = ConfigurationManager.AppSettings["Redis.AppName"]
            }));
        }
    }
}