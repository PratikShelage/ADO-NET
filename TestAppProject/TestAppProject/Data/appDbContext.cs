using Microsoft.EntityFrameworkCore;
using TestAppProject.Model;

namespace TestAppProject.Data
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Student> students { get; set; }


        protected appDbContext()
        {
        }
    }
}
