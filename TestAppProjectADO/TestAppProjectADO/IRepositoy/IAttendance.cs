using Core.Entities;
using TestAppProject.DTO;
using WebApi.DTO;
using WebApi.Entities;

namespace WebApi.IRepositoy
{
    public interface IAttendance
    {
        Task<IEnumerable<Attendance>> GetAllAsync(int page, int pageSize);
        Task AddAsync(Attendance model);

        Task<IEnumerable<Attendance>> GetAllAttendanceAsync();

    }
}
