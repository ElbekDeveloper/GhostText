using GhostText.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
