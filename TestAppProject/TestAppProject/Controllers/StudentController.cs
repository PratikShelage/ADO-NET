using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using TestAppProject.DTO;
using TestAppProject.Helper;
using TestAppProject.IRepository;
using TestAppProject.Model;


namespace TestAppProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        /// <summary>
        /// get student and pagging,sorting and searching
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost("StudentData")]
        public async Task<ActionResult<IEnumerable<Student>>> Get([FromBody] DesignDTO Model)
        {
            try
            {

                var users = await _studentRepository.GetAllAsync();
                if (users == null)
                    return BadRequest("User not found");


                if (!String.IsNullOrEmpty(Model.searchByName))
                {
                    users = users.Where(a => a.firstname.ToLower().Contains(Model.searchByName.ToLower()) || a.RollNO.ToString().Contains(Model.searchByName!.ToString())).ToList();

                }


                if (Model.StartDate.HasValue || Model.EndDate.HasValue)
                {

                    DateTime? fromDate = Model.StartDate;
                    DateTime? toDate = Model.EndDate;


                    users = users.Where(a =>
                    {

                        DateTime presentDate;
                        if (DateTime.TryParse(a.presentDate.ToString(), out presentDate))
                        {

                            return (!fromDate.HasValue || presentDate >= fromDate) &&
                                   (!toDate.HasValue || presentDate <= toDate);
                        }
                        return false;
                    }).ToList();
                }

                var data = Model.sortByName;

                if (Model.sortByType == "Asc")
                {
                    switch (data)
                    {

                        case "fullname":
                            users = users.OrderBy(a => a.firstname).ToList();
                            break;
                        case "lastname":
                            users = users.OrderBy(a => a.lastname).ToList();
                            break;
                        case "RollNo":
                            users = users.OrderBy(a => a.RollNO).ToList();
                            break;
                        case "StudentClass":
                            users = users.OrderBy(a => a.StudentClass).ToList();
                            break;
                        case "presentDate":
                            users = users.OrderBy(a => a.presentDate).ToList();
                            break;
                        case "id":
                            users = users.OrderBy(a => a.Id).ToList();
                            break;
                    }
                }
                else
                {
                    switch (data)
                    {

                        case "fullname":
                            users = users.OrderByDescending(a => a.firstname).ToList();
                            break;
                        case "lastname":
                            users = users.OrderByDescending(a => a.lastname).ToList();
                            break;
                        case "RollNO":
                            users = users.OrderByDescending(a => a.RollNO).ToList();
                            break;
                        case "StudentClass":
                            users = users.OrderByDescending(a => a.StudentClass).ToList();
                            break;
                        case "presentDate":
                            users = users.OrderByDescending(a => a.presentDate).ToList();
                            break;
                        case "id":
                            users = users.OrderByDescending(a => a.Id).ToList();
                            break;
                    }
                }

                if (Model.isPresent == true)
                {
                    users = users.Where(a => a.isPresent == true);
                }
                else if(Model.isAbsent == true)
                {
                    users = users.Where(a => a.isAbsent == true);
                }


                var count = users.Count();
                var presentCounts = users.Where(a => a.isPresent == true).Count();
                var absentCounts = users.Where(a => a.isAbsent == true).Count();
                users = users.Skip(((int)Model.page - 1!) * (int)Model.pageSize).Take((int)Model.pageSize).ToList();

                var response = new
                {
                    counts = count,
                    presentcounts = presentCounts,
                    absentCount = absentCounts,
                    userValue = users,

                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// get student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            try
            {
                var users = await _studentRepository.GetByIdAsync(id);
                if (users != null)
                {
                    return Ok(users);
                }
                return NotFound("User Not Found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Add Student
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentDTO Model)
        {
            try
            {


                var studentsValue = await _studentRepository.GetAllAsync();


                var checkemail = studentsValue.Where(a => a.RollNO == Model.RollNO && a.StudentClass == Model.StudentClass).FirstOrDefault();
                if (checkemail != null)
                {
                    return BadRequest(new { message = "Already Have this Roll no." });
                }
                var countValue = studentsValue.Count() + 1;
                await _studentRepository.AddAsync(new Student { firstname = Model.firstname, lastname = Model.lastname, RollNO = Model.RollNO, StudentClass = Model.StudentClass, presentDate = Model.presentDate, isAbsent = Model.isAbsent, isPresent = Model.isPresent, count = countValue });
                return Ok(new { message = "Student Added successfully", });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update the Student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Model"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentDTO Model)
        {
            try
            {
                var data = await _studentRepository.GetByIdAsync(id);


                var uservalue = await _studentRepository.GetAllAsync();

                if (data != null)
                {
                    data.firstname = Model.firstname;
                    data.lastname = Model.lastname;
                    data.RollNO = Model.RollNO;
                    data.StudentClass = Model.StudentClass;
                    data.presentDate = Model.presentDate;
                    data.isAbsent = Model.isAbsent;
                    data.isPresent = Model.isPresent;
                    await _studentRepository.UpdateAsync(data);
                    return Ok(new { message = "Student Updated Successfully." });
                }


                return NotFound("Student Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
        /// <summary>
        /// Delete Student data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var users = await _studentRepository.GetByIdAsync(id);
                if (users != null)
                {
                    await _studentRepository.DeleteAsync(users);

                    return Ok(new { messagev = "Student Deleted Successfully." });
                }
                return NotFound("Student Not Found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
