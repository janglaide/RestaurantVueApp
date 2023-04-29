using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult HomePage()
        {
            return Ok("Hello world");
        }
    }
}
