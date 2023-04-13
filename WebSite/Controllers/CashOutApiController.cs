using System.Web.Http;
using System.Web.Http.Cors;
using WebSite.Models.CashOut;

namespace WebSite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cash-out")]
    public class CashOutApiController : ApiController
    {
        [Route("VoidBet"), HttpPost]
        public IHttpActionResult VoidBet(VoidBetApiModel request)
        {
            return Ok(request);
        }

        [Route("UndoVoidBet"), HttpPost]
        public IHttpActionResult UndoVoidBet(VoidBetApiModel request)
        {
            return Ok(request);
        }

        [Route("CashOutResultUpdate"), HttpPost]
        public IHttpActionResult CashOutResultUpdate(CalcRequest request)
        {
            return Ok(request);
        }

        [Route("CashOutResultRefund"), HttpPost]
        public IHttpActionResult CashOutResultRefund(RefundRequest request)
        {
            return Ok(request);
        }

        [Route("Test"), HttpGet]
        [Route("test/{matchId}/{betstatus}")]
        public IHttpActionResult Test(int matchId, string betstatus)
        {
            return Ok($"{matchId} - {betstatus}");
        }
    }
}