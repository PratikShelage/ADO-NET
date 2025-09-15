using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppProject.DTO;
using WebApi.DTO;
using WebApi.Entities;

namespace Repo.IRepositoy
{
    public interface IStudenRepo
    {
        Task<Student> GetByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllAsync(DesignDTO model);
        Task<IEnumerable<Student>> GetAllStudent();

        Task<IEnumerable<Student>> GetAllAsyncwithoutSort(int page, int pageSize);

        Task AddAsync(StudentDTO model);
        Task UpdateAsync(int id,StudentDTO model);

        Task DeleteAsync(int id);
    }
}
