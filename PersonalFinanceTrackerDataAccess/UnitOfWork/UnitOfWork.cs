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
        private readonly IServiceProvider _serviceProvider;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(FinanceDbContext db, IServiceProvider serviceProvider)
        {
            _context = db;
            _serviceProvider = serviceProvider;
        }

        public TRepository GetRepository<TRepository>() where TRepository : class => (TRepository)_serviceProvider.GetRequiredService(typeof(TRepository));

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
