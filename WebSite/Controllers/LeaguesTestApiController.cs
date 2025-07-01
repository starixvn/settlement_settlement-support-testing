using Fanex.Caching;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Route("api/league-test")]
    [ApiController]
    public class LeaguesTestApiController(ICache cache) : ControllerBase
    {
        private const string KEY_LEAGUEID = "Settlement-AutoPayout-AutoInstant-LeagueId";

        private const string KEY_LEAGUEID_OTHERSPORT = "Settlement-AutoPayoutOtherSport-LeagueId";

        private const string KEY_LEAGUEID_VIRTUALCRICKET = "Settlement-AutoPayoutVirtualCricket-LeagueId";

        private const string KEY_LEAGUEIDS_TEST = "Settlement-AutoSettle-LeagueIds";

        private const double CACHE_EXPIRED_IN_DAYS = 1095;


        [HttpGet("get")]
        public IActionResult Get()
        {
            int leagueId, leagueIdVirtualCricket;
            string leagueIdOtherSport, leagueIdsTest;

            cache.TryGet(KEY_LEAGUEID, out leagueId);
            cache.TryGet(KEY_LEAGUEID_OTHERSPORT, out leagueIdOtherSport);
            cache.TryGet(KEY_LEAGUEID_VIRTUALCRICKET, out leagueIdVirtualCricket);
            cache.TryGet(KEY_LEAGUEIDS_TEST, out leagueIdsTest);
            return Ok(new { leagueId, leagueIdOtherSport, leagueIdVirtualCricket, leagueIdsTest });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] LeaguesTestModel model)
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

        [HttpPost("update-other-sport")]
        public IActionResult UpdateOtherSport([FromBody] LeaguesTestModel model)
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

        [HttpPost("update-virtual-cricket")]
        public IActionResult UpdateVirtualCricket([FromBody] LeaguesTestModel model)
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

        [HttpPost("update-any-sport")]
        public IActionResult UpdateAnySport([FromBody] LeaguesTestModel model)
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