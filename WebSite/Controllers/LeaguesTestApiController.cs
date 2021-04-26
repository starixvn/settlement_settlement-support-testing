using System;
using System.Configuration;
using System.Web.Http;
using Fanex.Caching;
using Fanex.Caching.Redis;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [RoutePrefix("api/league-test")]
    public class LeaguesTestApiController : ApiController
    {
        private const string KEY_EXCLUDE_LEAGUEID = "Settlement-AutoPayout-AutoInstant-ExcludeLeagueId";

        private const string KEY_INCLUDE_LEAGUEID = "Settlement-AutoPayout-AutoInstant-IncludeLeagueId";

        private const double CACHE_EXPIRED_IN_DAYS = 1095;

        private readonly LeaguesTestService leaguesTestService;

        private readonly ICache cache;

        public LeaguesTestApiController()
        {
            this.leaguesTestService = new LeaguesTestServiceImp();

            this.cache = new RedisCache(new RedisCacheOptions
            {
                ConnectionString = $"{ConfigurationManager.AppSettings["Redis.Server"]}:{ConfigurationManager.AppSettings["Redis.Port"]},password={ConfigurationManager.AppSettings["Redis.Password"]},defaultDatabase=11",
                ApplicationName = ConfigurationManager.AppSettings["Redis.AppName"]
            });
        }

        [HttpGet, Route("get")]
        public IHttpActionResult Get()
        {
            int excludeLeagueId, includeLeagueId;
            cache.TryGet(KEY_EXCLUDE_LEAGUEID, out excludeLeagueId);
            cache.TryGet(KEY_INCLUDE_LEAGUEID, out includeLeagueId);

            return Ok(new { includeLeagueId, excludeLeagueId });
        }

        [HttpPost, Route("update")]
        public IHttpActionResult Update(LeaguesTestModel model)
        {
            var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(CACHE_EXPIRED_IN_DAYS));
            if (model.IsClear)
            {
                cache.Set(KEY_EXCLUDE_LEAGUEID, 0, cachePolicy);
                cache.Set(KEY_INCLUDE_LEAGUEID, 165, cachePolicy);
            }
            else
            {
                var val = model.LeagueIds.Replace(" ", string.Empty);
                cache.Set(KEY_EXCLUDE_LEAGUEID, int.Parse(val), cachePolicy);
                cache.Set(KEY_INCLUDE_LEAGUEID, int.Parse(val), cachePolicy);
            }

            return Ok(new Result());
        }
    }
}