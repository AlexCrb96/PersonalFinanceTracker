using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Repositories
{
    public interface IRepository<T, TId>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TId id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
