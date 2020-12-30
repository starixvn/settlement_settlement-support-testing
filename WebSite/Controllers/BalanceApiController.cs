using System.Threading.Tasks;
using System.Web.Http;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
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