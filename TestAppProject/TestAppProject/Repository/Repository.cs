using Microsoft.EntityFrameworkCore;
using TestAppProject.Data;
using TestAppProject.IRepository;
namespace TestAppProject.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly appDbContext _Context;

        public Repository(appDbContext context)
        {
            _Context = context;
        }

        public async Task<T> GetByIdAsync(int id) => await _Context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => _Context.Set<T>().ToList();

        public async Task AddAsync(T enity)
        {

            await _Context.Set<T>().AddRangeAsync(enity);
            _Context.SaveChanges();

        }

        public async Task UpdateAsync(T enity)
        {

            _Context.Set<T>().Update(enity);
            _Context.SaveChanges();

        }

        public async Task DeleteAsync(T entity)
        {
            _Context.Set<T>().Remove(entity);
            _Context.SaveChanges();
        }


    }
}
