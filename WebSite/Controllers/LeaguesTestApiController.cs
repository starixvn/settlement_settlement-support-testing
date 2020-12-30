using System.Configuration;
using System.Web.Http;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [RoutePrefix("api/league-test")]
    public class LeaguesTestApiController : ApiController
    {
        private readonly LeaguesTestService leaguesTestService;

        public LeaguesTestApiController()
        {
            this.leaguesTestService = new LeaguesTestServiceImp();
        }

        [HttpGet, Route("get")]
        public IHttpActionResult Get()
        {
            var fileNameOnUat = ConfigurationManager.AppSettings["LeaguesTestUAT"];
            var fileNameOnPro = ConfigurationManager.AppSettings["LeaguesTestPRO"];

            var uat = leaguesTestService.ReadSetting(fileNameOnUat);
            var pro = leaguesTestService.ReadSetting(fileNameOnPro);
            return Ok(new { uat, pro });
        }

        [HttpPost, Route("update")]
        public IHttpActionResult Update(LeaguesTestModel model)
        {
            leaguesTestService.ChangeLeaguesTest(model);
            return Ok(new Result());
        }
    }
}