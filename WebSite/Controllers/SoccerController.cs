using Fanex.Data.Repository;
using System.Web.Http;
using WebSite.Common;
using WebSite.Constants;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [RoutePrefix("api/Match")]
    public class SoccerController : ApiController
    {
        private readonly SoccerResultService soccerResultService;

        public SoccerController()
        {
            var providerResultService = new ProviderResultServiceImp(new DynamicRepository());
            this.soccerResultService = new SoccerResultServiceImp(providerResultService);
        }

        [HttpPost, Route("goalRecord")]
        public IHttpActionResult GoalRecord(QueryModel model)
        {
            var valid = Validation.ValidResult(model);

            if (valid.ErrCode > 0)
            {
                return Json(valid);
            }

            var result = soccerResultService.Insert(model);
            return Json(result);
        }

        [HttpPost, Route("singleMatch")]
        public IHttpActionResult SingleMatch(QueryModel model)
        {
            var valid = Validation.ValidResult(model);

            if (valid.ErrCode > 0)
            {
                return Json(valid);
            }
            else
            {
                var result = soccerResultService.InsertSingleMatch(model);
                return Json(result);
            }
        }

        [HttpPost, Route("multipleMatch")]
        public IHttpActionResult MultipleMatch(ListQueryModel model)
        {
            var multiValid = Validation.MultiValidation(model);

            if (multiValid.ErrCode > 0)
            {
                return Json(multiValid);
            }
            else
            {
                var matchFail = soccerResultService.InsertMultiMatches(model.Matches);

                return string.IsNullOrWhiteSpace(matchFail)
                    ? Json(new Models.Result())
                    : Json(new Models.Result { ErrCode = ErrorCode.FailMatch, ErrMsg = ErrorMessage.MatchFailed + matchFail });
            }
        }
    }
}