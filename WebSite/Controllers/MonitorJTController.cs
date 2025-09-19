using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/monitor")]
    [ApiController]
    public class MonitorJTController : ControllerBase
    {
        [HttpPost("SendMarket")]
        public IActionResult SendMarket([FromBody] MarketRequest markets)
        {
            RequestMonitorService.AddMarket(markets.Request);
            return Ok();
        }

        [HttpGet("GetMarkets")]
        public IActionResult GetMarkets()
        {
            var requestData = RequestMonitorService.GetMarket();
            return Ok(requestData);
        }

        [HttpPost("ClearMarket")]
        public IActionResult ClearMarket([FromBody] Market market)
        {
            // Ensure the market object has valid data
            if (market == null || market.MarketId == string.Empty)
            {
                return BadRequest("Invalid market data");
            }
            RequestMonitorService.ClearMarket(market);
            return Ok();
        }

        [HttpPost("UpdateLiveScore")]
        public IActionResult UpdateLiveScore([FromBody] Market market)
        {
            if (market == null)
            {
                return BadRequest("Invalid request data");
            }

            RequestMonitorService.UpdateLiveScore(market.MarketId, market.LiveHomeScore, market.LiveAwayScore);
            return Ok();
        }

        [HttpPost("GetMarkets/{matchId}")]
        public IActionResult GetMarkets(int matchId)
        {
            var requestData = RequestMonitorService.GetMarket();
            requestData.ForEach(r => r.MatchId = matchId);
            return Ok(new MarketResponse
            {
                IsSuccess = true,
                Message = string.Empty,
                Data = requestData
            });
        }
    }
}