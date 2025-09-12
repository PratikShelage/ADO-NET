using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Repo.IRepositoy;
using Repo.Repository;
using System.Threading.Tasks;
using TestAppProject.DTO;
using WebApi.DTO;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudenRepo _studentRepo;

        public StudentController(IStudenRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [HttpPost("StudentData")]
        public async Task<ActionResult<IEnumerable<Student>>> Get([FromBody] DesignDTO model)
        {
            var data = await _studentRepo.GetAllAsync(model);
            var fullDetails = await _studentRepo.GetAllStudent();
            var count = fullDetails.Count();
            var presentCounts = fullDetails.Where(a => a.ispresent == true).Count();
            var absentCounts = fullDetails.Where(a => a.isabsent == true).Count();


            var response = new
            {
                counts = count,
                presentcounts = presentCounts,
                absentCount = absentCounts,
                userValue = data
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var data = await _studentRepo.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentDTO model)
        {
            await _studentRepo.AddAsync(model);
            return Ok(new { message = "user created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentDTO model)
        {
            await _studentRepo.UpdateAsync(id, model);
            return Ok(new { message = "user updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentRepo.DeleteAsync(id);
            return Ok(new { message = "user deleted successfully" });
        }
    }
}
