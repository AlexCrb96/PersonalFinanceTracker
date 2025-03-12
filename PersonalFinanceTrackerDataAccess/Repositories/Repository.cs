using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerDataAccess.DataAccessContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Repositories
{
    public class Repository<T, TId> : IRepository<T, TId> where T : class
    {
        protected readonly FinanceDbContext _db;
        protected readonly DbSet<T> _dbSet;

        public Repository(FinanceDbContext context)
        {
            _db = context;
            _dbSet = _db.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T?> GetByIdAsync(TId id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task SaveAsync() => await _db.SaveChangesAsync();
    }
}
