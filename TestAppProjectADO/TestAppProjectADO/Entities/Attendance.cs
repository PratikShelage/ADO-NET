namespace WebApi.Entities
{
    public class Attendance
    {
        public int aid { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public int rollno { get; set; }

        public string studentclass { get; set; }

        public string presentdate { get; set; }

        public Boolean ispresent { get; set; }
        public Boolean isabsent { get; set; }
    }
}
