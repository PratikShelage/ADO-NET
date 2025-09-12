using AngularAuthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthApi.Context
{
    public class applictaionDbContext : DbContext
    {
        public applictaionDbContext(DbContextOptions options) : base(options)
        {
        }

      public  DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");   
        }
    }
}
