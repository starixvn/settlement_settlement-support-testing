using System.Web.Http;
using System.Web.Http.Cors;

[EnableCors(origins: "*", headers: "*", methods: "*")]
[System.Web.Http.RoutePrefix("api/monitor")]
public class MonitorJTController : ApiController
{
    [Route("GetRequests"), HttpGet]
    public IHttpActionResult VoidBet()
    {
        var requestData = RequestMonitorService.GetRequests();
        return Ok(requestData);
    }

    [Route("SendMarket"), HttpPost]
    public IHttpActionResult SendMarket([FromBody] MarketRequest markets)
    {
        RequestMonitorService.AddMarket(markets.Request);
        return Ok();
    }

    [Route("GetMarkets"), HttpGet]
    public IHttpActionResult GetMarkets()
    {
        var requestData = RequestMonitorService.GetMarket();
        return Ok(requestData);
    }

    [Route("GetMarkets"), HttpPost]
    public IHttpActionResult GetMarkets([FromUri] int matchId)
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