using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

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
            string requestData = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            var query = new BetBuilderQuery
            {
                Service = new NotificationServiceIndex { Index = 0 },
                QueryResult = new BetBuilderQueryResult
                {
                    ResultCode = "SUCCESS",
                    Notifications = new BetBuilderParentNotifications
                    {
                        Offset = "10327",
                        Notifications = new List<BetBuilderNotification>
                        {
                            new BetBuilderNotification
                            {
                                BetBuilderOdds = new BetBuilderOdds
                                {
                                    Bet = new Bet
                                    {
                                        BetId = new BetId { Id = "0987654321" }
                                    },
                                    MatchId = new MatchId { Id = "0123456789" },
                                    Odds = 2.50
                                }
                            },
                            new BetBuilderNotification
                            {
                                BetBuilderOdds = new BetBuilderOdds
                                {
                                    Bet = new Bet
                                    {
                                        BetId = new BetId { Id = "1234567890" }
                                    },
                                    MatchId = new MatchId { Id = "9876543210" },
                                    Odds = 1.75
                                }
                            }
                        }
                    }
                }
            };

            return Ok(query);
        }
    }
}