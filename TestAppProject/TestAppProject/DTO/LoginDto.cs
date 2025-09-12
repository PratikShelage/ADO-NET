using System.ComponentModel.DataAnnotations;

namespace TestAppProject.DTO
{
    public class LoginDto
    {

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
