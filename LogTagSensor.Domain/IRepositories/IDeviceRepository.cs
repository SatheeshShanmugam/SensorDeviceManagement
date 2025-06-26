namespace LogTagSensor.Domain.IRepositories
{
    // Repository Interfaces
    public interface IDeviceRepository : IGenericRepository<Device>
    {

        Task<Device> GetDeviceBySerialNumberAsync(string serialNumber);


    }
}
