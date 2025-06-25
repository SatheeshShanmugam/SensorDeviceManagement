using LogTagSensor.Domain.IRepositories;
using LogTagSensor.Infrastructure.Data.DataContext;

namespace LogTagSensor.Infrastructure.Data.Repositories
{
    public class SensorReadingRepository : ISensorReadingRepository
    {
        private readonly DeviceMonitoringContext _context;

        public SensorReadingRepository(DeviceMonitoringContext context)
        {
            _context = context;
        }

        public async Task AddReadingAsync(SensorReading reading)
        {
            await _context.SensorReadings.AddAsync(reading);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasAlarmBeenTriggeredAsync(int deviceId, SensorType sensorType)
        {
            return await _context.Alarms
                .AnyAsync(a => a.DeviceId == deviceId &&
                             a.SensorType == sensorType &&
                             a.IsActive);
        }
    }
}
