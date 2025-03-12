using PersonalFinanceTrackerDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository, TEntity, TId>() where TEntity : class where TRepository : class, IRepository<TEntity, TId>;
        IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : class;
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveAsync();
    }
}
