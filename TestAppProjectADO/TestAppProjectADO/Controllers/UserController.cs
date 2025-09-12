using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Repo.IRepositoy;
using System.Collections;
using TestAppProject.DTO;
using WebApi.DTO;
using WebApi.IRepositoy;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IStudenRepo _studenRepo;
        public UserController(IUserRepo userRepo,IStudenRepo studenRepo)
        {
            _userRepo = userRepo;
            _studenRepo = studenRepo;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Get([FromBody] DesignDTO model)
        {
            try
            {
                var data = await _userRepo.GetAllAsync(model);
                var fulldeatils = await _userRepo.GetAllUser();
                var studentdata=await _studenRepo.GetAllAsync(model);
                var count = fulldeatils.Count();
                var maleCount = fulldeatils.Where(a => a.gender.ToLower() == "male").Count();
                var FemaleCount = fulldeatils.Where(a => a.gender.ToLower() == "female").Count();
                var response = new
                {
                    counts = count,
                    malecounts = maleCount,
                    FemaleCounts = FemaleCount,
                    user = data
                };
                return Ok(response);
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                var data = await _userRepo.GetByIdAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto model)
        {
            try
            {
                await _userRepo.UpdateAsync(id,model);
                return Ok(new { message = "user updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userRepo.DeleteAsync(id);
                return Ok(new {message ="user deleted successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
