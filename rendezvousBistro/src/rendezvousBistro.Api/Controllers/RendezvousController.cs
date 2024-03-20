using Microsoft.AspNetCore.Mvc;

namespace rendezvousBistro.Api.Controllers
{
    [Route("rendezvous")]
    public class RendezvousController : ApiController
    {
        [HttpGet]
        public IActionResult GetRendezvous()
        {
            return Ok(Array.Empty<string>());
        }
    }
}