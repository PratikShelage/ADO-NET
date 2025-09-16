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
        public async Task<ActionResult<IEnumerable<Attendance>>> Get([FromBody] int page, int pageSize, string attendancesearch)
        {
           
            var fullDetails = await _attendance.GetAllAttendanceAsync();
            var data = await _attendance.GetAllAsync(page, pageSize,attendancesearch);
            var count = fullDetails.Count();
            var presentCounts = fullDetails.Where(a => a.ispresent == true && Convert.ToDateTime(a.presentdate).Millisecond == DateTime.Today.Millisecond).Count();
            var absentCounts = fullDetails.Where(a => a.isabsent == true && Convert.ToDateTime(a.presentdate).Millisecond == DateTime.Today.Millisecond).Count();
            var response = new
            {
                counts = count,
                userValue = data,
                presentcounts = presentCounts,
                absentCount = absentCounts,
            };

            return Ok(response);
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

        [HttpGet("editAttendance")]
        public async Task<ActionResult<IEnumerable<Attendance>>> Getallvalue()
        {
            var data = await _attendance.GetAllAttendanceAsync();
            var absentCounts = data.Where(a => Convert.ToDateTime(a.presentdate).Millisecond == DateTime.Today.Millisecond).ToList();
            return Ok(absentCounts);
        }





    }
}
