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
        }

        private static void StartDataBaseSetting()
        {
            DbSettingProviderManager
              .StartNewSession()
              .WithSqlServer(ConfigurationManager.AppSettings["StoredProcedurePath"])
              .Run();
        }
    }
}