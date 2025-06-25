using LogTagSensor.Domain.IRepositories;
using LogTagSensor.Infrastructure.Data.DataContext;

namespace LogTagSensor.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeviceMonitoringContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(DeviceMonitoringContext context)
        {
            _context = context;

        }
        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            if (!_repositories.TryGetValue(typeof(TRepository), out var repository))
            {
                repository = Activator.CreateInstance(typeof(TRepository), _context) as TRepository;
                _repositories[typeof(TRepository)] = repository;
            }
            return repository as TRepository;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
