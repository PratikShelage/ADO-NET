using System.ComponentModel.DataAnnotations;

namespace AngularAuthApi.Models.Dto
{
    public class LoginDTO
    {
        [Required] public string username { get; set; } = string.Empty;
        [Required] public string password { get; set; } = string.Empty;
    }
}
