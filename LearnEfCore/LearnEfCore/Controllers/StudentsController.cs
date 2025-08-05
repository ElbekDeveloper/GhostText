using LearnEfCore.Data;
using LearnEfCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnEfCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext aplicationDbContext;

        public StudentsController(ApplicationDbContext applicationDbContext)
        {
            this.aplicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostUserAsync(Student student)
        {
            await this.aplicationDbContext.Students.AddAsync(student);

            await this.aplicationDbContext.SaveChangesAsync();

            return Ok(student);
        }
    }
}
