using TestAppProject.Data;
using TestAppProject.IRepository;
using TestAppProject.Model;

namespace TestAppProject.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(appDbContext context) : base(context)
        {
        }
    }
}
