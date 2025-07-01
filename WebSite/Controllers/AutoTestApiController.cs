using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/auto-test")]
    [ApiController]
    public class AutoTestApiController(QueryService queryService) : ControllerBase
    {
        [HttpPost("get-match-by-sport-type")]
        public IActionResult GetMatchBySportId([FromBody] SportTypeRequest request)
        {
            var matchId = queryService.GetMatchBySportType(request);
            return Ok(new { matchId });
        }

        [HttpGet("test")]
        public IActionResult TestMatchBySportId(int sporttype, bool isTicket)
        {
            var request = new SportTypeRequest
            {
                Id = sporttype,
                IsTicket = isTicket
            };

            var matchId = queryService.GetMatchBySportType(request);
            return Ok(new { matchId });
        }
    }
}