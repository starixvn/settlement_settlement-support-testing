using System.Threading.Tasks;
using System.Web.Http;
using WebSite.Models.ExternalResults;
using WebSite.Services;

namespace WebSite.Controllers
{
    [RoutePrefix("api/score")]
    public class ExternalResultsApiController : ApiController
    {
        private readonly QueryService queryService;

        public ExternalResultsApiController()
        {
            queryService = new QueryService();
        }

        [Route("push-result"), HttpPost]
        public async Task<IHttpActionResult> Push(QueryModel request)
        {
            return Ok(await queryService.PushScore(request));
        }
    }
}