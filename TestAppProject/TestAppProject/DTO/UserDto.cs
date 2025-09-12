using System.ComponentModel.DataAnnotations;

namespace TestAppProject.DTO
{
    public class UserDto
    {
        public ImageDto profilepic { get; set; }

        public string email { get; set; }

        public string password { get; set; }


        public DateOnly dob { get; set; }

        public string? age { get; set; }

        public string? gender { get; set; }


        public string? about { get; set; }

        public string[]? hobbies { get; set; }

        public string? firstname { get; set; }

        
        public string? middlename { get; set; }

        
        public string? lastname { get; set; }

        
        public string? phoneNo { get; set; }

      
        public string? address { get; set; }

 
        public string? landmark { get; set; }


        public string? pincode { get; set; }

     
        public string? country { get; set; }

  
        public string? state { get; set; }

   
        public string? city { get; set; }
    }
}
