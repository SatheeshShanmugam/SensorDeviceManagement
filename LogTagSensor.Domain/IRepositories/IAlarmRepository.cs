namespace LogTagSensor.Domain.IRepositories
{
    public interface IAlarmRepository
    {
        Task AddAlarmAsync(Alarm alarm);
        Task<IEnumerable<Alarm>> GetDeviceAlarmsAsync(int deviceId);
        Task<bool> DeviceHasActiveAlarmOfTypeAsync(int deviceId, SensorType sensorType);
    }
}
