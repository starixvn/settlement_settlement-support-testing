using Fanex.Data;
using Fanex.Data.SqlServer;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSite
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterRoutes(RouteTable.Routes);
            StartDataBaseSetting();
        }

        private static void StartDataBaseSetting()
        {
            DbSettingProviderManager
              .StartNewSession()
              .WithSqlServer(ConfigurationManager.AppSettings["StoredProcedurePath"])
              .Run();
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "BroadCast",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}