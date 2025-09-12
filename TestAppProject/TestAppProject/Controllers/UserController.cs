using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using TestAppProject.DTO;
using TestAppProject.Helper;
using TestAppProject.IRepository;
using TestAppProject.JwtToken;
using TestAppProject.Model;
using TestAppProject.Repository;


namespace TestAppProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _Data;
        private readonly GenerateJwtToken _jwtToken;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStudentRepository _Student;



        public UserController(IUserRepository userRepository, GenerateJwtToken token, IWebHostEnvironment webHostEnvironment, IStudentRepository studentRepository)
        {
            _Data = userRepository;
            _jwtToken = token;
            _webHostEnvironment = webHostEnvironment;
            _Student = studentRepository;
        }


        /// <summary>
        /// get userData and pagging,sorting and searching
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Get([FromBody] DesignDTO Model)
        {
            try
            {
                var students = await _Student.GetAllAsync();
                var users = await _Data.GetAllAsync();
                if (users == null)
                    return BadRequest("user not found");


                if (!String.IsNullOrEmpty(Model.searchByName))
                {
                    users = users.Where(a => a.firstname.ToLower().Contains(Model.searchByName.ToLower()) || a.email.ToString().Contains(Model.searchByName!.ToString())).ToList();

                }
                if (Model.StartDate.HasValue || Model.EndDate.HasValue)
                {

                    DateTime? fromDate = Model.StartDate;
                    DateTime? toDate = Model.EndDate;


                    students = students.Where(a =>
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

                        case "email":
                            users = users.OrderBy(a => a.email).ToList();
                            break;
                        case "dob":
                            users = users.OrderBy(a => a.dob).ToList();
                            break;
                        case "age":
                            users = users.OrderBy(a => a.age).ToList();
                            break;
                        case "gender":
                            users = users.OrderBy(a => a.gender).ToList();
                            break;
                        case "about":
                            users = users.OrderBy(a => a.about).ToList();
                            break;
                        case "fullname":
                            users = users.OrderBy(a => a.firstname).OrderBy(a => a.lastname).ToList();
                            break;
                        case "address":
                            users = users.OrderBy(a => a.address).ToList();
                            break;
                        case "country":
                            users = users.OrderBy(a => a.country).ToList();
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

                        case "email":
                            users = users.OrderByDescending(a => a.email).ToList();
                            break;
                        case "dob":
                            users = users.OrderByDescending(a => a.dob).ToList();
                            break;
                        case "age":
                            users = users.OrderByDescending(a => a.age).ToList();
                            break;
                        case "gender":
                            users = users.OrderByDescending(a => a.gender).ToList();
                            break;
                        case "about":
                            users = users.OrderByDescending(a => a.about).ToList();
                            break;
                        case "fullname":
                            users = users.OrderByDescending(a => a.firstname).OrderByDescending(a => a.lastname).ToList();
                            break;
                        case "address":
                            users = users.OrderByDescending(a => a.address).ToList();
                            break;
                        case "country":
                            users = users.OrderByDescending(a => a.country).ToList();
                            break;
                        case "id":
                            users = users.OrderByDescending(a => a.Id).ToList();
                            break;

                    }
                }

                ArrayList PresentList = new ArrayList();
                ArrayList AbsentList = new ArrayList();

                if (Model.StartDate.HasValue && Model.EndDate.HasValue)
                {
                    var start = Convert.ToInt32(Model.StartDate.Value.Day);
                    var end = Convert.ToInt32(Model.EndDate.Value.Day);

                    var startMonth = Model.StartDate.Value.Month;
                    var startYear = Model.StartDate.Value.Year;

                    for (var i = start; i <= end; i++)
                    {
                        var currentDate = new DateTime(startYear, startMonth, i);

                        var present = students.Where(a => a.presentDate.Day == i && a.presentDate.Month == startMonth && a.presentDate.Year == startYear && a.isPresent == true).ToList();
                        var absent = students.Where(a => a.presentDate.Day == i && a.presentDate.Month == startMonth && a.presentDate.Year == startYear && a.isPresent == false).ToList();

                        var formattedDate = currentDate.ToString("yyyy-MM-dd");

                        PresentList.Add(new attendancedata { label = formattedDate, y = present.Count() });
                        AbsentList.Add(new attendancedata { label = formattedDate, y = absent.Count() });
                    }
                }
                else
                {
                    var start = 1;
                    var end = 6;

                    var currentDate = DateTime.Now;

                    for (var i = start; i <= end; i++)
                    {
                      
                        var date = currentDate.AddDays(i - 1); 

                        var present = students.Where(a => a.presentDate.Day == i && a.isPresent == true).ToList();
                        var absent = students.Where(a => a.presentDate.Day == i && a.isAbsent == true).ToList();

                        var formattedDate = date.ToString("yyyy-MM-dd");

                        PresentList.Add(new attendancedata { label = formattedDate, y = present.Count() });
                        AbsentList.Add(new attendancedata { label = formattedDate, y = absent.Count() });
                    }
                }

                var count = users.Count();
                var maleCount = users.Where(a => a.gender.ToLower() == "male").Count();
                var FemaleCount = users.Where(a => a.gender.ToLower() == "female").Count();
                users = users.Skip(((int)Model.page - 1!) * (int)Model.pageSize).Take((int)Model.pageSize).ToList();

                var response = new
                {
                    counts = count,
                    malecounts = maleCount,
                    FemaleCounts = FemaleCount,
                    user = users,
                    presentcountarray = PresentList.ToArray(),
                    AbsentListCount = AbsentList.ToArray(),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                var users = await _Data.GetByIdAsync(id);
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
        /// Upadate the user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Model"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto Model)
        {
            try
            {
                var data = await _Data.GetByIdAsync(id);


                var uservalue = await _Data.GetAllAsync();

                if (data != null)
                {

                    data.profilepic = Model.profilepic;
                    data.email = Model.email;
                    data.password = PasswordHashing.HashPassword(Model.password);
                    data.dob = Model.dob;
                    data.age = Model.age;
                    data.gender = Model.gender;
                    data.about = Model.about;
                    data.hobbies = Model.hobbies;
                    data.firstname = Model.firstname;
                    data.middlename = Model.middlename;
                    data.lastname = Model.lastname;
                    data.phoneNo = Model.phoneNo;
                    data.address = Model.address;
                    data.landmark = Model.landmark;
                    data.pincode = Model.pincode;
                    data.country = Model.country;
                    data.state = Model.state;
                    data.city = Model.city;
                    await _Data.UpdateAsync(data);
                    return Ok(new { message = "User Updated Successfully." });
                }

                return NotFound("User Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }


        /// <summary>
        /// Remove the user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var users = await _Data.GetByIdAsync(id);
                if (users != null)
                {
                    await _Data.DeleteAsync(users);

                    return Ok(new { messagev = "User Deleted Successfully." });
                }
                return NotFound("User Not Found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}


public class attendancedata
{
    public string label { get; set; }

    public int y { get; set; }
}