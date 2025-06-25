using LogTagSensor.Domain.IRepositories;
using LogTagSensor.Infrastructure.Data.DataContext;

namespace LogTagSensor.Infrastructure.Data.Repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly DeviceMonitoringContext _context;

        public AlarmRepository(DeviceMonitoringContext context)
        {
            _context = context;
        }

        public async Task AddAlarmAsync(Alarm alarm)
        {
            await _context.Alarms.AddAsync(alarm);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Alarm>> GetDeviceAlarmsAsync(int deviceId)
        {
            return await _context.Alarms
                .Where(a => a.DeviceId == deviceId)
                .OrderByDescending(a => a.TriggerTime)
                .ToListAsync();
        }

        public async Task<bool> DeviceHasActiveAlarmOfTypeAsync(int deviceId, SensorType sensorType)
        {
            return await _context.Alarms
                .AnyAsync(a => a.DeviceId == deviceId &&
                             a.SensorType == sensorType &&
                             a.IsActive);
        }
    }
}
