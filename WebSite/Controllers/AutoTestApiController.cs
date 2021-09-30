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
        public IHttpActionResult GetMatchBySportId(SportTypeRequest request)
        {
            var matchId = queryService.GetMatchBySportType(request);
            return Ok(new { matchId });
        }

        [Route("test"), HttpGet]
        public IHttpActionResult TestMatchBySportId(int sporttype, bool isTicket)
        {
            var request = new SportTypeRequest
            {
                Id = sporttype,
                IsTicket = isTicket
            };

            var matchId = queryService.GetMatchBySportType(request);
            return Ok(new { matchId });
        }
    }
}