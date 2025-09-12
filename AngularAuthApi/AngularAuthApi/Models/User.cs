using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularAuthApi.Models
{
    public class User
    {
        [Key, Required]
        public int id { get; set; }
        [Required] public string firstname { get; set; } = string.Empty;
        [Required] public string lastname { get; set; } = string.Empty;
        [Required] public string email { get; set; } = string.Empty;
        [Required] public string username { get; set; } = string.Empty;
        [Required] public string password { get; set; } = string.Empty;
        public string? file { get; set; }
        public string? token { get; set; } = "admin";
        public string? role { get; set; } = "user";

        public string? resetPasswordToken { get; set; }

        public DateTime? resetpasswordExpiry { get; set; }
    }
}
