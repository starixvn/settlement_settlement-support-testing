using System.Web.Http;
using System.Web.Http.Cors;
using WebSite.Models;

namespace WebSite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/auto-test")]
    public class AutoTestApiController : ApiController
    {
        [Route("get-match-by-sport-type"), HttpPost]
        public IHttpActionResult GetMatchBySportId(SportTypeRequest sporttype)
        {
            var matchId = sporttype.Id;
            return Ok(new { matchId });
        }
    }
}