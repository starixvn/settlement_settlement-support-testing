using System.Web.Http;
using System.Web.Http.Cors;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Event")]
    public class DeleteMatchTestApiController : ApiController
    {
        private readonly QueryService service;

        public DeleteMatchTestApiController()
        {
            service = new QueryService();
        }

        [Route("Delete"), HttpPost]
        public IHttpActionResult DeleteMatch(DeleteMatchRequest request)
        {
            return Ok(service.DeleteMatch(request));
        }
    }
}