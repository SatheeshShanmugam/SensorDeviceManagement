namespace LogTagSensor.Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>() where TRepository : class;        
        Task<int> CompleteAsync();
    }
}
