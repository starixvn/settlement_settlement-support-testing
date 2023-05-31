using Fanex.Caching;
using Fanex.Caching.Redis;
using System.Configuration;
using System.Web.Http;

namespace WebSite.Controllers
{
    [RoutePrefix("api/monitoring-refresh-function")]
    public class MonitoringRefreshApiController : ApiController
    {
        private readonly ICache cache;
        private const string HitKey = "Hit-Settlement";
        private const string MissKey = "Miss-Settlement";
        private const string ErrorKey = "Error-Settlement";

        public MonitoringRefreshApiController()
        {
            this.cache = new RedisCache(new RedisCacheOptions
            {
                ConnectionString = $"{ConfigurationManager.AppSettings["Redis.Server"]}:{ConfigurationManager.AppSettings["Redis.Port"]},password={ConfigurationManager.AppSettings["Redis.Password"]},defaultDatabase=13",
                ApplicationName = ConfigurationManager.AppSettings["Redis.AppName"]
            });
        }

        [HttpGet, Route("get")]
        public IHttpActionResult Get()
        {
            cache.TryGet(HitKey, out string hit);
            cache.TryGet(MissKey, out string miss);
            cache.TryGet(ErrorKey, out string error);

            return Ok(new { hit, miss, error });
        }
    }
}