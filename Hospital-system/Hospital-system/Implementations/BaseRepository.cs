using Hospital_system.Data;
using Hospital_system.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital_system.Implementations
{
    public class Repository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly DbSet<T> _DbSet;
        public Repository(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            _DbSet = _DbContext.Set<T>();
        }
        public IQueryable<T> GetAll() => _DbSet.AsNoTracking().AsQueryable();
        public async Task<T?> GetByID(string id) => await _DbSet.FindAsync(id);

        public async Task AddAsync(T entity) => await _DbSet.AddAsync(entity);

        public void Update(T entity) => _DbSet.Update(entity);

        public void Delete(T entity) => _DbSet.Remove(entity);
        //{
        //    var record = _DbSet.Find(id);
        //    if (record != null)
        //        _DbSet.Remove(record);
        //}
        public async Task SaveAsync() => await _DbContext.SaveChangesAsync();

    }
}
