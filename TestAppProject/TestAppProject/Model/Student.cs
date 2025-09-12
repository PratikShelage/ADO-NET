using System.ComponentModel.DataAnnotations;

namespace TestAppProject.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        public int RollNO { get; set; }

        [Required]
        public string StudentClass { get; set; }

        public DateOnly presentDate { get; set; }

        public Boolean? isPresent { get; set; } = false;
        public Boolean? isAbsent { get; set; } = false;

        public int? count { get; set; }


    }
}
