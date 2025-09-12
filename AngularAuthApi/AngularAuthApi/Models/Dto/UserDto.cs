using System.ComponentModel.DataAnnotations;

namespace AngularAuthApi.Models.Dto
{
    public class UserDto
    {
        [Required] public string firstname { get; set; } = string.Empty;
        [Required] public string lastname { get; set; } = string.Empty;
        [Required] public string email { get; set; } = string.Empty;
        [Required] public string username { get; set; } = string.Empty;
        [Required] public string password { get; set; } = string.Empty;
        public IFormFile file { get; set; }
        public string? token { get; set; } = "admin";
        public string? role { get; set; } = "user";
    }
}
