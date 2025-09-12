using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    [Owned]
    public class ImageDTO
    {
        public string? name { get; set; }
        public string? base64string { get; set; }
    }
}
