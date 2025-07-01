using Microsoft.AspNetCore.Mvc;
using WebSite.Models.ExternalResults;
using WebSite.Services;

namespace WebSite.Controllers
{
    [Route("api/score")]
    [ApiController]
    public class ExternalResultsApiController(QueryService queryService) : ControllerBase
    {
        [HttpPost("push-result")]
        public async Task<IActionResult> Push([FromBody] QueryModel request)
        {
            return Ok(await queryService.PushScore(request));
        }
    }
}