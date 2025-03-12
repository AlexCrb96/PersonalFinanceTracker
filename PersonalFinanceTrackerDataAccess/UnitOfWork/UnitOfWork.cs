using Microsoft.EntityFrameworkCore.Storage;
using PersonalFinanceTrackerDataAccess.DataAccessContext;
using PersonalFinanceTrackerDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private IDbContextTransaction? _transaction;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(FinanceDbContext db, IServiceProvider serviceProvider)
        {
            _context = db;
            _serviceProvider = serviceProvider;
        }

        public TRepository GetRepository<TRepository, TEntity, TId>() where TEntity : class where TRepository : class, IRepository<TEntity, TId>
        {
            if (!_repositories.TryGetValue(typeof(TEntity), out var outputRepo))
            {
                // Try to get custom repository from DI if it's registered
                outputRepo = _serviceProvider.GetService(typeof(TRepository)) as TRepository;

                // If custom repository is not registered, use default repository
                if (outputRepo == null)
                {
                    outputRepo = new Repository<TEntity, TId>(_context);
                }

                _repositories.Add(typeof(TEntity), outputRepo);
            }

            return (TRepository)outputRepo;
        }

        public IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : class => GetRepository<Repository<TEntity, TId>, TEntity, TId>();

        public async Task BeginTransactionAsync() => _transaction = await _context.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            await _transaction.CommitAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _context.Dispose();
        }

    }
}
