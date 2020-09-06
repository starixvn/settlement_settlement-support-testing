using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/clone-data")]
    public class CloneApiController : ApiController
    {
        private readonly QueryService service;

        public CloneApiController()
        {
            service = new QueryService();
        }

        [Route("get-result"), HttpPost]
        public async Task<IHttpActionResult> GetResultDetail(GetResultDetailRequest request)
        {
            return Ok(await service.Get(request.MatchId));
        }

        [Route("clone-result"), HttpPost]
        public async Task<IHttpActionResult> CloneResult(CloneResultDetailModel request)
        {
            return Ok(await service.Clone(request));
        }
    }
}