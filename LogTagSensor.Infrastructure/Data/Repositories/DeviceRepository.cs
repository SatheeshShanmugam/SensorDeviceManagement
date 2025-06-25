using LogTagSensor.Domain.IRepositories;
using LogTagSensor.Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LogTagSensor.Infrastructure.Data.Repositories
{// Repository Implementations
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        private readonly DeviceMonitoringContext _context;

        public DeviceRepository(DeviceMonitoringContext context):base(context)
        {
            _context = context;
        }

        private Expression<Func<Device, bool>> isDeviceActive(int Id)
        {
            return d => d.Id == Id && d.IsActive;
        }

        private readonly Expression<Func<Device, bool>> isDeviceExists = d => d.IsActive;

        public async Task<Device> GetDeviceById(int Id)
        {
            return await GetAll(x => x.Thresholds).FirstOrDefaultAsync(isDeviceActive(Id));
        }

        public async Task<Device> GetDeviceBySerialNumberAsync(string serialNumber)
        {
            if (string.IsNullOrEmpty(serialNumber))
            {
                throw new ArgumentException("Serial number cannot be null or empty.", nameof(serialNumber));
            }

            // Fetch the device along with its thresholds using Include
             return await GetAll(x=> x.Thresholds).FirstOrDefaultAsync(d => d.SerialNumber == serialNumber);
        }
        

        public async Task UpdateDeviceAsync(Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }
    }
}
