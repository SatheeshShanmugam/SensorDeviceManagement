namespace LogTagSensor.Domain.IRepositories
{
    public interface ISensorReadingRepository
    {
        Task AddReadingAsync(SensorReading reading);
        Task<bool> HasAlarmBeenTriggeredAsync(int deviceId, SensorType sensorType);
    }
}
