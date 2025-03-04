using System.Web.Http;
using System.Web.Http.Cors;

namespace WebSite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cash-out")]
    public class CashOutApiController : ApiController
    {
        [Route("VoidBet"), HttpPost]
        public IHttpActionResult VoidBet(object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "VoidBet");
            return Ok(request);
        }

        [Route("UndoVoidBet"), HttpPost]
        public IHttpActionResult UndoVoidBet(object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "UndoVoidBet");
            return Ok(request);
        }

        [Route("CashOutResultUpdate"), HttpPost]
        public IHttpActionResult CashOutResultUpdate(object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "CashOutResultUpdate");
            return Ok(request);
        }

        [Route("CashOutResultRefund"), HttpPost]
        public IHttpActionResult CashOutResultRefund(object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "CashOutResultRefund");
            return Ok(request);
        }

        [Route("JackTimeCashOut/ResultMarkets"), HttpPut]
        public IHttpActionResult ResultMarkets(object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "ResultMarkets");
            return Ok(request);
        }

        [Route("GetMarkets"), HttpGet]
        public IHttpActionResult GetMarkets(int matchIds)
        {
            RequestMonitorService.AddRequest("MatchId: " + matchIds, "GetMarkets");
            var requestData = RequestMonitorService.GetMarket();
            requestData.ForEach(r => r.MatchId = matchIds);
            return Ok(new MarketResponse
            {
                IsSuccess = true,
                Message = string.Empty,
                Data = requestData
            });
        }

        [Route("Test"), HttpGet]
        [Route("test/{matchId}/{betstatus}")]
        public IHttpActionResult Test(int matchId, string betstatus)
        {
            return Ok($"{matchId} - {betstatus}");
        }
    }
}