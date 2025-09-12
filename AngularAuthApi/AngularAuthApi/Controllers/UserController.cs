using AngularAuthApi.Context;
using AngularAuthApi.Helpers;
using AngularAuthApi.Models;
using AngularAuthApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using AngularAuthApi.Utility;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly applictaionDbContext _context; //Direct Access of DbContext
        private readonly IConfiguration _configuration; // Access appsetting.json
        private readonly IEmail _emailService; // access Email DI
        public UserController(applictaionDbContext context, IConfiguration configuration, IEmail emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        /// <summary>
        /// 1:-this is use for authenticate the user with username and password 
        /// 2:-after authenticate its call the CreateJwtToken for JWT Token
        /// 3:-we use the PasswordHashing.VerifyPassword static method to verify the Hash Password
        /// </summary>


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO userObj)
        {
            try
            {
                if (userObj == null)
                    return BadRequest(new { message = "UserModel is null" });

                var userDetails = await _context.users.FirstOrDefaultAsync(i => i.username == userObj.username);

                if (userDetails == null)
                {
                    return BadRequest(new { message = "Username not found." });
                }

                var userpassword = await _context.users.FirstOrDefaultAsync(i => i.username == userObj.username);
                var Token = CreateJwtToken(userDetails);

                if (!PasswordHashing.VerifyPassword(userObj.password, userpassword.password))
                {
                    return BadRequest(new { message = "password not found." });
                }
                return Ok(new { Token = $"{Token}", message = "Login Succss." });


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// 1:- we check four condition to registor a user ,userObj,username,email,PasswordStrength
        /// 2:- we hash the password for new User
        /// </summary>



        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromForm] UserDto userObj)
        {
            try
            {
                if (userObj == null)
                    return BadRequest(new { message = "UserModel is null" });

                if (userObj.file == null && userObj.file.Length < 0)
                {
                    return BadRequest(new { message = "File is invalid" });
                }

                var folderName = Path.Combine("Resource", "AllFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                var filename = userObj.file.FileName;
                var fullPath = Path.Combine(pathToSave, filename);
                var dbpath = Path.Combine(folderName, filename);

                if (System.IO.File.Exists(dbpath))
                {
                    return BadRequest(new { message = "File is already exits" });
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    userObj.file.CopyTo(stream);
                }

                var data = _context.users.Where(a => a.username == userObj.username).FirstOrDefault();
                if (data != null)
                {
                    return BadRequest(new { message = "username is already exits" });
                }

                var data2 = _context.users.Where(a => a.email == userObj.email).FirstOrDefault(); ;
                if (data2 != null)
                {
                    return BadRequest(new { message = "email is already exits" });
                }

                var passCheck = CheckPasswordStrength(userObj.password);
                if (!(passCheck == "Strong Password"))
                {
                    return BadRequest(new { message = $"{passCheck}" });
                }

                var userDetails = await _context.users.FirstOrDefaultAsync(i => i.username == userObj.username && i.password == userObj.password);

                if (userDetails == null)
                {
                    userObj.password = PasswordHashing.HashPassword(userObj.password);
                    userObj.role = "User";
                    userObj.token = "Token";
                    var userData = await _context.users.AddAsync(new User { firstname = userObj.firstname, lastname = userObj.lastname, email = userObj.email, username = userObj.username, password = userObj.password, file = userObj.file.FileName.ToString(), role = userObj.role, token = userObj.token });
                    await _context.SaveChangesAsync();
                    return Ok(new { message = $"{userData}", Resopnse = "Registration Success." });
                }
                return BadRequest(new { message = "User already exits" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Download  a file using name 
        /// </summary>
   

        [HttpGet("download/{name}")]
        public async Task<IActionResult> DownloadByName(String name)
        {
            try
            {
                var folderName = Path.Combine("Resource", "AllFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var filename = name + ".jpeg";
                var fullpath = Path.Combine(pathToSave, filename);

                if (!System.IO.File.Exists(fullpath))
                {
                    return BadRequest(new { message = "file not exits" });
                }
                var fileBytes = await System.IO.File.ReadAllBytesAsync(fullpath);
                var fileContentResult = new FileContentResult(fileBytes, "application/octet-")
                {
                    FileDownloadName = fullpath + filename,
                };
                return fileContentResult;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //C:\Users\Pratik\source\repos\AngularAuthApi\AngularAuthApi\Resource\AllFiles\pratik.jpeg
        /// <summary>
        /// check password strength
        /// </summary>

        private string CheckPasswordStrength(string password)
        {
            try
            {
                if (password.Length >= 8 && Regex.IsMatch(password, "[@]"))
                {
                    return "Strong Password";
                }
                else
                {
                    return "Password should Contain 'min 8 char','Alphanumeric','Special Char'";
                }
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }

        }


        /// <summary>
        /// create the jwt Token 
        /// </summary>

        private string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceretasd23123123wqaS21312QE2SQAE12E12");


            var identity = new ClaimsIdentity(new Claim[]
         {
                new Claim (ClaimTypes.Role ,user.role!),
                new Claim(ClaimTypes.Name,user.username)
         });
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var Token = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = creds

            };

            var token = jwtTokenHandler.CreateToken(Token);
            return jwtTokenHandler.WriteToken(token);
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUSers()
        {
            try
            {
                var user = await _context.users.ToListAsync();
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// use For to send email for those user who wants to reset password 
        /// </summary>


        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            try
            {
                var user = await _context.users.FirstOrDefaultAsync(a => a.email == email);
                if (user != null)
                {
                    var tokenBytes = RandomNumberGenerator.GetBytes(64);
                    var emailToken = Convert.ToBase64String(tokenBytes);
                    user.resetPasswordToken = emailToken;
                    user.resetpasswordExpiry = DateTime.UtcNow.AddMinutes(50);
                    string from = _configuration["EmailSettings:SmtpServer"]!;
                    var emailModel = new EmailDTO(email, "Reset Password!!", EmailBody.EmailStringBody(email, emailToken));
                    _emailService.SendEmail(emailModel);
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

        /// <summary>
        /// 1:- find the user first and then hash the password and update the old password
        /// </summary>


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
