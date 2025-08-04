using Microsoft.AspNetCore.Mvc;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHome() 
        {
            return Content("Hello, World!");
        }
    }
}
