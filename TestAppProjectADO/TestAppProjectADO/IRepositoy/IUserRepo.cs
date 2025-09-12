using Core.Entities;
using TestAppProject.DTO;
using WebApi.DTO;

namespace WebApi.IRepositoy
{
    public interface IUserRepo
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync(DesignDTO? model);

        Task<IEnumerable<User>> GetAllUser();
        Task AddAsync(UserDto model);
        Task UpdateAsync(int id, UserDto model);

        Task DeleteAsync(int id);
    }
}
