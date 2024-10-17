using Fanex.Caching;
using Fanex.Caching.Redis;
using System;
using System.Configuration;
using System.Web.Http;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [RoutePrefix("api/league-test")]
    public class LeaguesTestApiController : ApiController
    {
        private const string KEY_LEAGUEID = "Settlement-AutoPayout-AutoInstant-LeagueId";

        private const string KEY_LEAGUEID_OTHERSPORT = "Settlement-AutoPayoutOtherSport-LeagueId";

        private const string KEY_LEAGUEID_VIRTUALCRICKET = "Settlement-AutoPayoutVirtualCricket-LeagueId";

        private const string KEY_LEAGUEIDS_TEST = "Settlement-AutoSettle-LeagueIds";

        private const double CACHE_EXPIRED_IN_DAYS = 1095;

        private readonly LeaguesTestService leaguesTestService;

        private readonly ICache cache;

        public LeaguesTestApiController()
        {
            this.leaguesTestService = new LeaguesTestServiceImp();

            this.cache = new RedisCache(new RedisCacheOptions
            {
                ConnectionString = $"{ConfigurationManager.AppSettings["Redis.Server"]}:{ConfigurationManager.AppSettings["Redis.Port"]},password={ConfigurationManager.AppSettings["Redis.Password"]},defaultDatabase={ConfigurationManager.AppSettings["Redis.DefaultDatabase"]}",
                ApplicationName = ConfigurationManager.AppSettings["Redis.AppName"]
            });
        }

        [HttpGet, Route("get")]
        public IHttpActionResult Get()
        {
            int leagueId, leagueIdVirtualCricket;
            string leagueIdOtherSport, leagueIdsTest;

            cache.TryGet(KEY_LEAGUEID, out leagueId);
            cache.TryGet(KEY_LEAGUEID_OTHERSPORT, out leagueIdOtherSport);
            cache.TryGet(KEY_LEAGUEID_VIRTUALCRICKET, out leagueIdVirtualCricket);
            cache.TryGet(KEY_LEAGUEIDS_TEST, out leagueIdsTest);
            return Ok(new { leagueId, leagueIdOtherSport, leagueIdVirtualCricket, leagueIdsTest });
        }

        [HttpPost, Route("update")]
        public IHttpActionResult Update(LeaguesTestModel model)
        {
            var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(CACHE_EXPIRED_IN_DAYS));
            var val = model.LeagueIds.Replace(" ", string.Empty);
            if (model.IsClear)
            {
                cache.Set(KEY_LEAGUEID, 0, cachePolicy);
            }
            else
            {
                cache.Set(KEY_LEAGUEID, int.Parse(val), cachePolicy);
            }

            return Ok(new Result());
        }

        [HttpPost, Route("update-other-sport")]
        public IHttpActionResult UpdateOtherSport(LeaguesTestModel model)
        {
            var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(CACHE_EXPIRED_IN_DAYS));
            var val = model.LeagueIds.Replace(" ", string.Empty);
            if (model.IsClear)
            {
                cache.Set(KEY_LEAGUEID_OTHERSPORT, "", cachePolicy);
            }
            else
            {
                cache.Set(KEY_LEAGUEID_OTHERSPORT, val, cachePolicy);
            }

            return Ok(new Result());
        }

        [HttpPost, Route("update-virtual-cricket")]
        public IHttpActionResult UpdateVirtualCricket(LeaguesTestModel model)
        {
            var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(CACHE_EXPIRED_IN_DAYS));
            var val = model.LeagueIds.Replace(" ", string.Empty);
            if (model.IsClear)
            {
                cache.Set(KEY_LEAGUEID_VIRTUALCRICKET, 0, cachePolicy);
            }
            else
            {
                cache.Set(KEY_LEAGUEID_VIRTUALCRICKET, int.Parse(val), cachePolicy);
            }

            return Ok(new Result());
        }

        [HttpPost, Route("update-any-sport")]
        public IHttpActionResult UpdateAnySport(LeaguesTestModel model)
        {
            var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(CACHE_EXPIRED_IN_DAYS));
            var val = model.LeagueIds.Replace(" ", string.Empty);
            if (model.IsClear)
            {
                cache.Set(KEY_LEAGUEIDS_TEST, string.Empty, cachePolicy);
            }
            else
            {
                cache.Set(KEY_LEAGUEIDS_TEST, val, cachePolicy);
            }

            return Ok(new Result());
        }
    }
}