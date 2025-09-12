using System.ComponentModel.DataAnnotations;

namespace TestAppProject.DTO
{
    public class StudentDTO
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int RollNO { get; set; }
        public string StudentClass { get; set; }
        public DateOnly presentDate { get; set; }

        public Boolean? isPresent { get; set; } = false;
        public Boolean? isAbsent { get; set; } = false;

        //public int? count { get; set; }
    }
}
