using Microsoft.AspNetCore.Mvc;
using WebSite.Common;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers;

[ApiController]
[Route("api/match")]
public class SoccerController(SoccerResultService soccerResultService) : ControllerBase
{
    [HttpGet("Index")]
    public IActionResult Index()
    {
        return Content("OK");
    }

    [HttpPost("goal-record")]
    public IActionResult GoalRecord([FromBody] QueryModel model)
    {
        var valid = Validation.ValidResult(model);

        if (valid.ErrCode > 0)
        {
            return Ok(valid);
        }

        var result = soccerResultService.Insert(model);
        return Ok(result);
    }

    [HttpPost("single-match")]
    public IActionResult SingleMatch([FromBody] QueryModel model)
    {
        var valid = Validation.ValidResult(model);

        if (valid.ErrCode > 0)
        {
            return Ok(valid);
        }
        else
        {
            var result = soccerResultService.InsertSingleMatch(model);
            return Ok(result);
        }
    }
}