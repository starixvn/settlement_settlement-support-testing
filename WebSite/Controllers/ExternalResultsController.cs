using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebSite.Models.ExternalResults;
using WebSite.Services;

namespace WebSite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/score")]
    public class ExternalResultsController : ApiController
    {
        private readonly QueryService queryService;

        public ExternalResultsController()
        {
            queryService = new QueryService();
        }

        [Route("push-result"), HttpPost]
        public async Task<IHttpActionResult> Push(QueryModel request)
        {
            return Ok(await queryService.PushScore(request));
        }
    }
}