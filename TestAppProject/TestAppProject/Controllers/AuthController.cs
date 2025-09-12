using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;
using TestAppProject.Data;
using TestAppProject.DTO;
using TestAppProject.Helper;
using TestAppProject.IRepository;
using TestAppProject.JwtToken;
using TestAppProject.Model;
using TestAppProject.Utility;
using static System.Net.Mime.MediaTypeNames;

namespace TestAppProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly appDbContext _context;
        private readonly IUserRepository _Data;
        private readonly GenerateJwtToken _jwtToken;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IEmail _emailService;

        public AuthController(appDbContext appDbContext,IUserRepository userRepository, GenerateJwtToken token, IWebHostEnvironment webHostEnvironment, IConfiguration configuration,IEmail emailService)
        {
            _context = appDbContext;
            _Data = userRepository;
            _jwtToken = token;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _emailService = emailService;
        }
        /// <summary>
        /// User Login and create Jwt Token
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost("Authentication")]
        public async Task<IActionResult> Login([FromBody] LoginDto Model)
        {
            try
            {
                var users = await _Data.GetAllAsync();

                if (users != null)
                {
                    var checkemail = users.Where(a => a.email == Model.email).FirstOrDefault();
                    if (checkemail == null)
                    {
                        return BadRequest(new { message = "email not found" });
                    }

                    var checkpassword = checkemail.password;
                    ;
                    if (!PasswordHashing.VerifyPassword(Model.password, checkpassword))
                    {
                        return BadRequest(new { message = "paasword is incorrect" });
                    }
                    var token = _jwtToken.createJWtToken(checkemail);
                    return Ok(new { message = "user login successfully", Token = token });


                }

                return NotFound("User Not Found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Create User and hashpassword
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>

        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] UserDto Model)
        {
            try
            {
                if (Model != null)
                {
                    var uservalue = await _Data.GetAllAsync();

                    var checkEmail = uservalue.Where(a => a.email == Model.email).FirstOrDefault();
                    if (checkEmail != null)
                    {
                        return BadRequest(new { message = "Email is Already Exits" });
                    }

                    var userValue = new User
                    {
                        profilepic = Model.profilepic,
                        email = Model.email,
                        password = PasswordHashing.HashPassword(Model.password),
                        dob = Model.dob,
                        age = Model.age,
                        gender = Model.gender,
                        about = Model.about,
                        hobbies = Model.hobbies,
                        firstname = Model.firstname,
                        middlename = Model.middlename,
                        lastname = Model.lastname,
                        phoneNo = Model.phoneNo,
                        address = Model.address,
                        landmark = Model.landmark,
                        pincode = Model.pincode,
                        country = Model.country,
                        state = Model.state,
                        city = Model.city,
                        role = "admin"
                    };

                    await _Data.AddAsync(userValue);
                    return Ok(new { message = "User Added Successfully." });
                }
                return NotFound("User Not Found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmails(string email)
        {
            try
            {
                var users = await _Data.GetAllAsync();
                var user = users.FirstOrDefault(a=>a.email == email);
                if (user != null)
                {
                    var tokenBytes = RandomNumberGenerator.GetBytes(64);
                    var emailToken = Convert.ToBase64String(tokenBytes);
                    user.resetPasswordToken = emailToken;
                    user.resetpasswordExpiry = DateTime.UtcNow.AddMinutes(50);
                    string from = _configuration["EmailSettings:SmtpServer"]!;
                    var emailModel = new EmailDTO(email, "Reset Password!!", EmailBody.EmailStringBody(email, emailToken));
                    //await _emailService.SendMail(emailModel);
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Email Send!" });

                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> resetpassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                var newToken = resetPasswordDTO.emailToken.Replace(" ", "+");
                var user = await _context.users.AsNoTracking().FirstOrDefaultAsync(a => a.email == resetPasswordDTO.email);
                if (user != null)
                {
                    var Token = user.resetPasswordToken;
                    DateTime emailTokenExpory = Convert.ToDateTime(user.resetpasswordExpiry);
                    //if (Token != resetPasswordDTO.emailToken || emailTokenExpory < DateTime.Now)
                    //{
                    //    return BadRequest(new {message ="Innvalid reset link"});
                    //}
                    user.password = PasswordHashing.HashPassword(resetPasswordDTO.password);
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Password is reset" });
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
