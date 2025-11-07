using Fanex.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using WebSite.Models.CashOut;
using WebSite.Services;

namespace WebSite.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/JackTimeCashOut")]
    [AllowAnonymous]
    [ApiController]
    public class CashOutApiController : ControllerBase
    {
        [HttpPost("VoidBet")]
        public IActionResult VoidBet([FromBody] object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "VoidBet");
            return Ok(request);
        }

        [HttpPost("UndoVoidBet")]
        public IActionResult UndoVoidBet([FromBody] object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "UndoVoidBet");
            return Ok(request);
        }

        [HttpPost("CashOutResultUpdate")]
        public IActionResult CashOutResultUpdate([FromBody] object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "CashOutResultUpdate");
            return Ok(request);
        }

        [HttpPost("CashOutResultRefund")]
        public IActionResult CashOutResultRefund([FromBody] object request)
        {
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            RequestMonitorService.AddRequest(requestData, "CashOutResultRefund");
            return Ok(request);
        }

        [HttpGet("Markets")]
        public IActionResult GetMarkets(int matchIds)
        {
            RequestMonitorService.AddRequest("MatchId: " + matchIds, "GetMarkets");
            var requestData = RequestMonitorService.GetMarket();
            requestData.ForEach(r => r.MatchId = matchIds);
            return Ok(new MarketResponse
            {
                IsSuccess = true,
                Message = string.Empty,
                // Data = requestData
            });
        }

        [HttpGet("Test")]
        [HttpGet("test/{matchId}/{betstatus}")]
        public IActionResult Test(int matchId, string betstatus)
        {
            return Ok($"{matchId} - {betstatus}");
        }

        [HttpPost("Query")]
        public IActionResult NotificationsQuery([FromBody] object request)
        {
            var betBuilderQueryService = new BetBuilderQueryService();
            var query = betBuilderQueryService.CreateSampleQuery();

            return Ok(query);
        }

        [HttpPost("command")]
        public IActionResult Command([FromBody] object request)
        {
            LogService.InfoAsync("Received command: " + Newtonsoft.Json.JsonConvert.SerializeObject(request));
            var response = new VoidLegsResponse
            {
                Header = new VoidLegsHeader
                {
                    ReplyStatusCode = "SUCCESS"
                }
            };
            return Ok(response);
        }
    }
}