using Microsoft.EntityFrameworkCore;
using TestAppProject.Data;
using TestAppProject.DTO;
using TestAppProject.IRepository;
using TestAppProject.Model;

namespace TestAppProject.Repository
{
    public class StudentRepository:Repository<Student>,IStudentRepository
    {
        public StudentRepository(appDbContext context) : base(context)
        {
        }

    }
}
