using Microsoft.AspNetCore.Mvc;

namespace BlogNet.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController: ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
