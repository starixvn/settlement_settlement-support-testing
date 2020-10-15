using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/balance")]
    public class BalanceApiController : ApiController
    {
        private readonly QueryService service;

        public BalanceApiController()
        {
            service = new QueryService();
        }

        [Route("get-balance"), HttpPost]
        public async Task<IHttpActionResult> GetBalance(GetBalanceRequest request)
        {
            return Ok(await service.GetBalance(request));
        }
    }
}