using Microsoft.EntityFrameworkCore;

namespace TestAppProject.DTO
{
    [Owned]
    public class ImageDto
    {
        public string? name {  get; set; }
        public string? base64string { get; set; }
    }
}
