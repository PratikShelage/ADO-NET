using ADO.NETCRUD.Helper;
using ADO.NETCRUD.Jwt;
using Microsoft.AspNetCore.Mvc;
using TestAppProject.DTO;
using WebApi.DTO;
using WebApi.IRepositoy;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepo _userRepo;

        public AuthController(JwtSettings jwtSettings, IUserRepo userRepo)
        {
            _jwtSettings = jwtSettings;
            _userRepo = userRepo;
        }

        [HttpPost("Authentication")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {
                var data = await _userRepo.GetAllUser();
                if (data == null)
                    return NotFound("User not found");  
                var checkemail = data.Where(a=>a.email == model.email).FirstOrDefault();
                if (checkemail == null)
                {
                    return BadRequest("Email is Incorrect");
                }
                
                if (!PasswordHashing.VerifyPassword(model.password, checkemail.password))
                {
                    return BadRequest("Password is Incorrect");
                }

                var token = _jwtSettings.CreateJwtToken(checkemail);

                return Ok(new { message = "user Register successfully.", Token = token });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] UserDto model)
        {
            try
            {
                await _userRepo.AddAsync(model);
                return Ok(new { message = "user created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
