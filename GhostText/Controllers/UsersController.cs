using System.Linq;
using System.Threading.Tasks;
using GhostText.Models.Users;
using GhostText.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService) =>
            this.userService = userService;

        [HttpPost]
        public async ValueTask<ActionResult<User>> PostUserAsync([FromBody] User user) =>
            StatusCode(201, await this.userService.AddUserAsync(user));

        [HttpGet]
        public ActionResult<IQueryable<User>> GetAllUsers() =>
            Ok(this.userService.RetrieveAllUsers());
    }
}
