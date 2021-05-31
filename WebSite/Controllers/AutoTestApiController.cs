using System.Web.Http;
using System.Web.Http.Cors;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/auto-test")]
    public class AutoTestApiController : ApiController
    {
        private readonly QueryService queryService;

        public AutoTestApiController()
        {
            queryService = new QueryService();
        }

        [Route("get-match-by-sport-type"), HttpPost]
        public IHttpActionResult GetMatchBySportId(SportTypeRequest sporttype)
        {
            var matchId = queryService.GetMatchBySportType(sporttype.Id);
            return Ok(new { matchId });
        }

        [Route("test"), HttpGet]
        public IHttpActionResult TestMatchBySportId(int sporttype)
        {
            var matchId = queryService.GetMatchBySportType(sporttype);
            return Ok(new { matchId });
        }
    }
}