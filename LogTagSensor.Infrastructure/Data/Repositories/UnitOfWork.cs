using LogTagSensor.Domain.IRepositories;
using LogTagSensor.Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace LogTagSensor.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeviceMonitoringContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly IServiceProvider _serviceProvider;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DeviceMonitoringContext context, IServiceProvider serviceProvider, IDbTransaction transaction)
        {
            _context = context;
            _serviceProvider = serviceProvider;

        }
        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            if (!_repositories.TryGetValue(typeof(TRepository), out var repository))
            {
                repository = _serviceProvider.GetRequiredService<TRepository>();
                _repositories[typeof(TRepository)] = repository;
            }
            return repository as TRepository;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransacation()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransaction()
        {
            try
            {
                if (_transaction != null)
                {
                    await CompleteAsync();
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        // Rollback the transaction
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await DisposeTransactionAsync();
            }
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
