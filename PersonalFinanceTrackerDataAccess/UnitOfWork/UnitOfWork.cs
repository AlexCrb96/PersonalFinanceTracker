using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
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
        private IDbContextTransaction? _transaction;

        private readonly Dictionary<Type, object> _customRepos = new();

        public UnitOfWork(FinanceDbContext db, IServiceProvider serviceProvider)
        {
            _context = db;

            _customRepos[typeof(UserRepository)] = new UserRepository(_context);
        }

        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            if (_customRepos.TryGetValue(typeof(TRepository), out object repo))
            {
                return (TRepository)repo;
            }
            throw new InvalidOperationException($"Repository of type {typeof(TRepository).Name} not found in custom repositories dictionary.");
        }

        public IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : class => new Repository<TEntity, TId>(_context);

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
