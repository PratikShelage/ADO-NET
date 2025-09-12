using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using TestAppProject.DTO;

namespace TestAppProject.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ImageDto profilepic { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]

        public DateOnly? dob { get; set; } 

        [Required]
        public string? age { get; set; }

        [Required]
        public string ? gender { get; set; }  

        [Required]

        public string ? about { get; set; }

        [Required]
        public string[] ? hobbies{ get; set; }


        [Required]
        public string? firstname { get; set; }

        [Required]
        public string? middlename { get; set; }

        [Required]
        public string? lastname { get; set; }

        [Required]
        public string? phoneNo { get; set; }

        [Required]
        public string? address { get; set; }

        [Required]
        public  string? landmark { get; set; }

        [Required]
        public string? pincode { get; set; }

        [Required]
        public string? country { get; set; }

        [Required]
        public string? state { get; set; }

        [Required]
        public string? city { get; set; }

        public string? role { get; set; }

        public string? resetPasswordToken { get; set; }

        public DateTime? resetpasswordExpiry { get; set; }
    }
}

