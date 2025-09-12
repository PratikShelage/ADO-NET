using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Student
    {
        public int sid { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public int rollno { get; set; }

        public string studentclass { get; set; }

        public string presentdate { get; set; }

        public Boolean ispresent { get; set; }
        public Boolean isabsent { get; set; }

    }
}
