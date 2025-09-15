using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Repo.IRepositoy;
using Repo.Repository;
using TestAppProject.DTO;
using WebApi.DTO;
using WebApi.Entities;
using WebApi.IRepositoy;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class attendanceController : ControllerBase
    {

        private readonly IAttendance _attendance;

        public attendanceController(IAttendance attendance)
        {
            _attendance = attendance;
        }

        [HttpPost("AttendanceData")]
        public async Task<ActionResult<IEnumerable<Attendance>>> Get([FromBody] int page, int pageSize)
        {
            var data = await _attendance.GetAllAsync(page,pageSize);  

            return Ok(data);
        }

        [HttpPost]      
        public async Task<IActionResult> Post([FromBody] Attendance model)
        {
            await _attendance.AddAsync(model);
            return Ok(new { message = "user attendance created for today" });
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> Getall()
        {
            var data = await _attendance.GetAllAttendanceAsync();

            return Ok(data);
        }




    }
}
